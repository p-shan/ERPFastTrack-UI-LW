namespace ERPFastTrack.DBGround.DBModels.Custom
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedByUserId { get; set; }

        // Navigation property

        public ICollection<OrgUsersRelationship> OrgUsers { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Job> Jobs { get; set; }


        public ICollection<SalesforceConnection> SalesforceConfigurations { get; set; }
        public ICollection<FileSourceConnection> FileSourceConnections { get; set; }
        public ICollection<QueryConfiguration> QueryConfigurations { get; set; }
        public ICollection<DatabaseConnection> DatabaseConnections { get; set; }
        public ICollection<Scheduling> Schedulings { get; set; }
        public ICollection<Execution> Executions { get; set; }

    }
}
