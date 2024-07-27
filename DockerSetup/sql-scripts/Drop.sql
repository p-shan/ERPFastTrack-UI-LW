-- Set the schema name
DECLARE @schemaName NVARCHAR(128) = 'dbo'
DECLARE @sql NVARCHAR(MAX) = N''
DECLARE @crlf CHAR(2) = CHAR(13) + CHAR(10)

-- Disable all constraints
DECLARE @disableConstraints NVARCHAR(MAX) = N''
SELECT @disableConstraints += 'ALTER TABLE [' + s.name + '].[' + t.name + '] NOCHECK CONSTRAINT ALL;' + @crlf
FROM sys.tables t
JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE s.name = @schemaName

EXEC sp_executesql @disableConstraints

-- Temporary table to store the results of the CTE
CREATE TABLE #TableDependencies (
    TableName NVARCHAR(128),
    SchemaName NVARCHAR(128),
    Level INT
)

-- Recursive CTE to find the drop order
;WITH TableDependencies AS (
    SELECT 
        t.name AS TableName,
        s.name AS SchemaName,
        0 AS Level
    FROM 
        sys.tables t
    JOIN 
        sys.schemas s ON t.schema_id = s.schema_id
    LEFT JOIN 
        sys.foreign_keys fk ON t.object_id = fk.parent_object_id
    WHERE 
        fk.parent_object_id IS NULL AND s.name = @schemaName
    UNION ALL
    SELECT 
        t.name,
        s.name,
        td.Level + 1
    FROM 
        sys.tables t
    JOIN 
        sys.schemas s ON t.schema_id = s.schema_id
    JOIN 
        sys.foreign_keys fk ON t.object_id = fk.parent_object_id
    JOIN 
        TableDependencies td ON fk.referenced_object_id = OBJECT_ID(td.SchemaName + '.' + td.TableName)
    WHERE 
        s.name = @schemaName
)
-- Insert the results into the temporary table
INSERT INTO #TableDependencies
SELECT DISTINCT 
    TableName, SchemaName, Level
FROM 
    TableDependencies

-- Generate drop statements without duplicates
SELECT 
    @sql = @sql + 'DROP TABLE [' + SchemaName + '].[' + TableName + '];' + @crlf
FROM 
    #TableDependencies
ORDER BY 
    Level DESC, TableName

-- Print the SQL for debugging
PRINT @sql

-- Execute the drop statements
EXEC sp_executesql @sql

-- Enable all constraints
DECLARE @enableConstraints NVARCHAR(MAX) = N''
SELECT @enableConstraints += 'ALTER TABLE [' + s.name + '].[' + t.name + '] WITH CHECK CHECK CONSTRAINT ALL;' + @crlf
FROM sys.tables t
JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE s.name = @schemaName

EXEC sp_executesql @enableConstraints

-- Drop the temporary table
DROP TABLE #TableDependencies
