using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.API.Internals.Controllers.InternalBase.Operations;
using ERPFastTrack.APIModels;
using ERPFastTrack.APIModels.OperationsModels.Request;
using ERPFastTrack.APIModels.OperationsModels.Response;
using ERPFastTrack.DBGround.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.API.Internals.Controllers.Exposed
{
    public class MappingItem
    {
        public string Key { get; set; }
        public ValueItem Value { get; set; }
    }

    public class ValueItem
    {
        public bool ExternalFlag { get; set; }
        public bool LookupFlag { get; set; }
        public string Value { get; set; }
        public string LookupId { get; set; }
        public string LookupName { get; set; }
    }

    public class ApiResponse : BaseResponse
    {
        public string ObjectName { get; set; }
        public string TableName { get; set; }
        public string SalesforceId { get; set; }
        public string ConfigurationType { get; set; }
        public List<MappingItem> Mapping { get; set; }
        public List<string> LookupsToLoad { get; set; }
    }


    public class FieldDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public object sftype { get; set; }
        public string type { get; set; }
        public string label { get; set; }
        public bool isPrimaryKey { get; set; }
        public int length { get; set; }
        public bool isNullable { get; set; }
        public bool externalId { get; set; }
        public bool nillable { get; set; }
        public bool custom { get; set; }
        public bool createable { get; set; }
        public string soapType { get; set; }
        public bool defaultedOnCreate { get; set; }
        public List<string> referenceTo { get; set; }
    }

    public class ObjectDetails
    {
        public string name { get; set; }
        public List<FieldDetail> fields { get; set; }
    }

    public class ApiResponse2 : BaseResponse
    {
        public object errMessage { get; set; }
        public bool status { get; set; }
        public ObjectDetails objectDetails { get; set; }
    }

    public class ApiResponse3 : BaseResponse
    {
        public List<FieldData> data { get; set; }
    }
    public class FieldData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsExternalId { get; set; }
        public bool IsLookup { get; set; }
        public string LookupName { get; set; }
    }

    public class ColumnsResponse : BaseResponse
    {
        public List<string> Columns { get; set; }
    }

    public class ApiResponse4 : BaseResponse
    {
        public List<SObject> Data { get; set; }
    }

    public class SObject
    {
        public string Name { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        public DummyController()
        {

        }

        [HttpGet("sfdbobjectmapping")]
        public async Task<ApiResponse> getsfdbobjectmapping()
        {
            string jsonData = @"{
""ObjectName"": ""Order"",
    ""TableName"": ""SalesOrderHeader"",
    ""SalesforceId"": ""1234567"",
    ""ConfigurationType"": ""SF_DB_ObjectMapping"",
    ""Mapping"": [
        {
            ""Key"": ""SalesOrder"",
            ""Value"": {
                ""ExternalFlag"": true,
                ""LookupFlag"": false,
                ""Value"": ""SOKey__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""Customer"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": true,
                ""Value"": ""AccountId"",
                ""LookupId"": ""CustKey__c"",
                ""LookupName"": ""Account""
            }
        },
        {
            ""Key"": ""Company"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Company__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""OrderType"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Trans_Type__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""OrderDate"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""EffectiveDate"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""BillAddress"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""BillingStreet"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""BillCity"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""BillingCity"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""BillState"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""BillingState"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""BillZipCode"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""BillingPostalCode"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""BillCountry"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""BillingCountry"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ShipAddress1"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""ShippingStreet"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ShipState"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""ShippingState"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ShipCity"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""ShippingCity"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ShipZipCode"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""ShippingPostalCode"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ShipCountry"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""ShippingCountry"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ProjectDesc"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Project_Description__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""SpreadTon"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Spread_Ton__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""SpreadTonPay"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Spread_Ton_Pay__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""SpreadHour"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Spread_Hr__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""SpreadHourPay"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Spread_Hr_Pay__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""TranID"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Trans_Id__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""TranAmt"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Trans_Amt__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ExpirationDate"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Expiration_Date__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""Compliance"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Compliance__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""BidNo"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Bid_No__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ContractYear"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Contract_Year__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""ContractTerm"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Contract_Term__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""JobState"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Job_State__c"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""OrderStatus"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": false,
                ""Value"": ""Status"",
                ""LookupId"": null,
                ""LookupName"": null
            }
        },
        {
            ""Key"": ""PRICEBOOK2ID"",
            ""Value"": {
                ""ExternalFlag"": false,
                ""LookupFlag"": true,
                ""Value"": ""Pricebook2Id"",
                ""LookupId"": ""Id"",
                ""LookupName"": ""Pricebook2""
            }
        }
    ],
    ""LookupsToLoad"": [
        ""Account"",
        ""Pricebook2""
    ]
	}";

            return JObject.Parse(jsonData).ToObject<ApiResponse>();
        }

        [HttpGet("sfobjectdata")]
        public async Task<ApiResponse4> getsfobjectdata()
        {
            string jsonData = @"{
    ""data"": [
        {
            ""name"": ""AIApplication""
        },
        {
            ""name"": ""AIApplicationConfig""
        },
        {
            ""name"": ""AIInsightAction""
        },
        {
            ""name"": ""AIInsightFeedback""
        },
        {
            ""name"": ""AIInsightReason""
        },
        {
            ""name"": ""AIInsightValue""
        },
        {
            ""name"": ""AIPredictionEvent""
        },
        {
            ""name"": ""AIRecordInsight""
        },
        {
            ""name"": ""AcceptedEventRelation""
        },
        {
            ""name"": ""Account""
        },
        {
            ""name"": ""AccountBrand""
        },
        {
            ""name"": ""AccountBrandShare""
        },
        {
            ""name"": ""AccountChangeEvent""
        },
        {
            ""name"": ""AccountContactRole""
        },
        {
            ""name"": ""AccountContactRoleChangeEvent""
        },
        {
            ""name"": ""AccountFeed""
        },
        {
            ""name"": ""AccountHistory""
        },
        {
            ""name"": ""AccountPartner""
        },
        {
            ""name"": ""AccountShare""
        },
        {
            ""name"": ""ActionLinkGroupTemplate""
        },
        {
            ""name"": ""ActionLinkTemplate""
        },
        {
            ""name"": ""ActionPlan""
        },
        {
            ""name"": ""ActionPlanFeed""
        },
        {
            ""name"": ""ActionPlanItem""
        },
        {
            ""name"": ""ActionPlanItemDependency""
        },
        {
            ""name"": ""ActionPlanShare""
        },
        {
            ""name"": ""ActionPlanTemplate""
        },
        {
            ""name"": ""ActionPlanTemplateItem""
        },
        {
            ""name"": ""ActionPlanTemplateItemValue""
        },
        {
            ""name"": ""ActionPlanTemplateShare""
        },
        {
            ""name"": ""ActionPlanTemplateVersion""
        },
        {
            ""name"": ""ActionPlanTmplItmAssessmentInd""
        },
        {
            ""name"": ""ActionPlanTmplPkgConfig""
        },
        {
            ""name"": ""ActionPlnTmplItmDependency""
        },
        {
            ""name"": ""ActiveFeatureLicenseMetric""
        },
        {
            ""name"": ""ActivePermSetLicenseMetric""
        },
        {
            ""name"": ""ActiveProfileMetric""
        },
        {
            ""name"": ""ActivityFieldHistory""
        },
        {
            ""name"": ""ActivityHistory""
        },
        {
            ""name"": ""AdditionalNumber""
        },
        {
            ""name"": ""Address""
        },
        {
            ""name"": ""AdvAccountForecastFact""
        },
        {
            ""name"": ""AdvAccountForecastFactFeed""
        },
        {
            ""name"": ""AdvAccountForecastFactHistory""
        },
        {
            ""name"": ""AdvAccountForecastFactShare""
        },
        {
            ""name"": ""AdvAcctForecastFactAdj""
        },
        {
            ""name"": ""AdvAcctForecastFactAdjShare""
        },
        {
            ""name"": ""AdvAcctForecastSetPartner""
        },
        {
            ""name"": ""AdvAcctForecastSetPartnerFeed""
        },
        {
            ""name"": ""AdvAcctForecastSetPartnerHistory""
        },
        {
            ""name"": ""AdvAcctForecastSetPartnerShare""
        },
        {
            ""name"": ""AdvAcctForecastSetUse""
        },
        {
            ""name"": ""AdvAcctForecastSetUseFeed""
        },
        {
            ""name"": ""AdvAcctForecastSetUseHistory""
        },
        {
            ""name"": ""AdvAcctForecastSetUseShare""
        },
        {
            ""name"": ""AggregateResult""
        },
        {
            ""name"": ""AlternativePaymentMethod""
        },
        {
            ""name"": ""AlternativePaymentMethodShare""
        },
        {
            ""name"": ""Announcement""
        },
        {
            ""name"": ""ApexClass""
        },
        {
            ""name"": ""ApexComponent""
        },
        {
            ""name"": ""ApexEmailNotification""
        },
        {
            ""name"": ""ApexLog""
        },
        {
            ""name"": ""ApexPage""
        },
        {
            ""name"": ""ApexPageInfo""
        },
        {
            ""name"": ""ApexTestQueueItem""
        },
        {
            ""name"": ""ApexTestResult""
        },
        {
            ""name"": ""ApexTestResultLimits""
        },
        {
            ""name"": ""ApexTestRunResult""
        },
        {
            ""name"": ""ApexTestSuite""
        },
        {
            ""name"": ""ApexTrigger""
        },
        {
            ""name"": ""ApexTypeImplementor""
        },
        {
            ""name"": ""ApiAnomalyEvent""
        },
        {
            ""name"": ""ApiAnomalyEventStore""
        },
        {
            ""name"": ""ApiAnomalyEventStoreFeed""
        },
        {
            ""name"": ""ApiEvent""
        },
        {
            ""name"": ""ApiEventStream""
        },
        {
            ""name"": ""AppAnalyticsQueryRequest""
        },
        {
            ""name"": ""AppDefinition""
        },
        {
            ""name"": ""AppMenuItem""
        },
        {
            ""name"": ""AppTabMember""
        },
        {
            ""name"": ""AppUsageAssignment""
        },
        {
            ""name"": ""AssessmentIndicatorDefinition""
        },
        {
            ""name"": ""AssessmentIndicatorDefinitionFeed""
        },
        {
            ""name"": ""AssessmentIndicatorDefinitionHistory""
        },
        {
            ""name"": ""AssessmentIndicatorDefinitionShare""
        },
        {
            ""name"": ""AssessmentTask""
        },
        {
            ""name"": ""AssessmentTaskContentDocument""
        },
        {
            ""name"": ""AssessmentTaskContentDocumentFeed""
        },
        {
            ""name"": ""AssessmentTaskContentDocumentHistory""
        },
        {
            ""name"": ""AssessmentTaskDefinition""
        },
        {
            ""name"": ""AssessmentTaskDefinitionFeed""
        },
        {
            ""name"": ""AssessmentTaskDefinitionHistory""
        },
        {
            ""name"": ""AssessmentTaskDefinitionShare""
        },
        {
            ""name"": ""AssessmentTaskFeed""
        },
        {
            ""name"": ""AssessmentTaskHistory""
        },
        {
            ""name"": ""AssessmentTaskIndDefinition""
        },
        {
            ""name"": ""AssessmentTaskIndDefinitionFeed""
        },
        {
            ""name"": ""AssessmentTaskIndDefinitionHistory""
        },
        {
            ""name"": ""AssessmentTaskOrder""
        },
        {
            ""name"": ""AssessmentTaskOrderFeed""
        },
        {
            ""name"": ""AssessmentTaskOrderHistory""
        },
        {
            ""name"": ""AssessmentTaskShare""
        },
        {
            ""name"": ""Asset""
        },
        {
            ""name"": ""AssetAction""
        },
        {
            ""name"": ""AssetActionSource""
        },
        {
            ""name"": ""AssetChangeEvent""
        },
        {
            ""name"": ""AssetFeed""
        },
        {
            ""name"": ""AssetHistory""
        },
        {
            ""name"": ""AssetRelationship""
        },
        {
            ""name"": ""AssetRelationshipFeed""
        },
        {
            ""name"": ""AssetRelationshipHistory""
        },
        {
            ""name"": ""AssetStatePeriod""
        },
        {
            ""name"": ""AssetTokenEvent""
        },
        {
            ""name"": ""AssignmentRule""
        },
        {
            ""name"": ""AssociatedLocation""
        },
        {
            ""name"": ""AssociatedLocationHistory""
        },
        {
            ""name"": ""AsyncApexJob""
        },
        {
            ""name"": ""AsyncOperationEvent""
        },
        {
            ""name"": ""AsyncOperationLog""
        },
        {
            ""name"": ""AsyncOperationStatus""
        },
        {
            ""name"": ""AttachedContentDocument""
        },
        {
            ""name"": ""Attachment""
        },
        {
            ""name"": ""Audience""
        },
        {
            ""name"": ""AuraDefinition""
        },
        {
            ""name"": ""AuraDefinitionBundle""
        },
        {
            ""name"": ""AuraDefinitionBundleInfo""
        },
        {
            ""name"": ""AuraDefinitionInfo""
        },
        {
            ""name"": ""AuthConfig""
        },
        {
            ""name"": ""AuthConfigProviders""
        },
        {
            ""name"": ""AuthProvider""
        },
        {
            ""name"": ""AuthSession""
        },
        {
            ""name"": ""AuthorizationForm""
        },
        {
            ""name"": ""AuthorizationFormConsent""
        },
        {
            ""name"": ""AuthorizationFormConsentChangeEvent""
        },
        {
            ""name"": ""AuthorizationFormConsentHistory""
        },
        {
            ""name"": ""AuthorizationFormConsentShare""
        },
        {
            ""name"": ""AuthorizationFormDataUse""
        },
        {
            ""name"": ""AuthorizationFormDataUseHistory""
        },
        {
            ""name"": ""AuthorizationFormDataUseShare""
        },
        {
            ""name"": ""AuthorizationFormHistory""
        },
        {
            ""name"": ""AuthorizationFormShare""
        },
        {
            ""name"": ""AuthorizationFormText""
        },
        {
            ""name"": ""AuthorizationFormTextFeed""
        },
        {
            ""name"": ""AuthorizationFormTextHistory""
        },
        {
            ""name"": ""BackgroundOperation""
        },
        {
            ""name"": ""BackgroundOperationResult""
        },
        {
            ""name"": ""BatchApexErrorEvent""
        },
        {
            ""name"": ""BatchCalcJobDefinition""
        },
        {
            ""name"": ""BatchCalcJobDefinitionView""
        },
        {
            ""name"": ""BatchDataSource""
        },
        {
            ""name"": ""BatchDataSrcFilterCriteria""
        },
        {
            ""name"": ""BatchJob""
        },
        {
            ""name"": ""BatchJobDefinition""
        },
        {
            ""name"": ""BatchJobFeed""
        },
        {
            ""name"": ""BatchJobHistory""
        },
        {
            ""name"": ""BatchJobPart""
        },
        {
            ""name"": ""BatchJobPartFailedRecord""
        },
        {
            ""name"": ""BatchJobPartFailedRecordFeed""
        },
        {
            ""name"": ""BatchJobPartFailedRecordHistory""
        },
        {
            ""name"": ""BatchJobPartFeed""
        },
        {
            ""name"": ""BatchJobPartHistory""
        },
        {
            ""name"": ""BatchJobShare""
        },
        {
            ""name"": ""BatchJobStatusChangedEvent""
        },
        {
            ""name"": ""BatchProcessJobDefView""
        },
        {
            ""name"": ""BatchProcessJobDefinition""
        },
        {
            ""name"": ""BrandTemplate""
        },
        {
            ""name"": ""BrandingSet""
        },
        {
            ""name"": ""BrandingSetProperty""
        },
        {
            ""name"": ""BriefcaseAssignment""
        },
        {
            ""name"": ""BriefcaseAssignmentChangeEvent""
        },
        {
            ""name"": ""BriefcaseDefinition""
        },
        {
            ""name"": ""BriefcaseDefinitionChangeEvent""
        },
        {
            ""name"": ""BriefcaseRule""
        },
        {
            ""name"": ""BriefcaseRuleFilter""
        },
        {
            ""name"": ""BulkApiResultEvent""
        },
        {
            ""name"": ""BulkApiResultEventStore""
        },
        {
            ""name"": ""BusinessBrand""
        },
        {
            ""name"": ""BusinessBrandShare""
        },
        {
            ""name"": ""BusinessHours""
        },
        {
            ""name"": ""BusinessProcess""
        },
        {
            ""name"": ""CalcProcStepRelationship""
        },
        {
            ""name"": ""CalcProcStepRelationshipFeed""
        },
        {
            ""name"": ""CalcProcStepRelationshipHistory""
        },
        {
            ""name"": ""CalculationMatrix""
        },
        {
            ""name"": ""CalculationMatrixColumn""
        },
        {
            ""name"": ""CalculationMatrixColumnFeed""
        },
        {
            ""name"": ""CalculationMatrixColumnHistory""
        },
        {
            ""name"": ""CalculationMatrixFeed""
        },
        {
            ""name"": ""CalculationMatrixHistory""
        },
        {
            ""name"": ""CalculationMatrixRow""
        },
        {
            ""name"": ""CalculationMatrixRowFeed""
        },
        {
            ""name"": ""CalculationMatrixRowHistory""
        },
        {
            ""name"": ""CalculationMatrixShare""
        },
        {
            ""name"": ""CalculationMatrixVersion""
        },
        {
            ""name"": ""CalculationMatrixVersionFeed""
        },
        {
            ""name"": ""CalculationMatrixVersionHistory""
        },
        {
            ""name"": ""CalculationProcedure""
        },
        {
            ""name"": ""CalculationProcedureFeed""
        },
        {
            ""name"": ""CalculationProcedureHistory""
        },
        {
            ""name"": ""CalculationProcedureShare""
        },
        {
            ""name"": ""CalculationProcedureStep""
        },
        {
            ""name"": ""CalculationProcedureStepFeed""
        },
        {
            ""name"": ""CalculationProcedureStepHistory""
        },
        {
            ""name"": ""CalculationProcedureVariable""
        },
        {
            ""name"": ""CalculationProcedureVariableFeed""
        },
        {
            ""name"": ""CalculationProcedureVariableHistory""
        },
        {
            ""name"": ""CalculationProcedureVersion""
        },
        {
            ""name"": ""CalculationProcedureVersionFeed""
        },
        {
            ""name"": ""CalculationProcedureVersionHistory""
        },
        {
            ""name"": ""Calendar""
        },
        {
            ""name"": ""CalendarView""
        },
        {
            ""name"": ""CalendarViewShare""
        },
        {
            ""name"": ""CallCenter""
        },
        {
            ""name"": ""CallCoachingMediaProvider""
        },
        {
            ""name"": ""Campaign""
        },
        {
            ""name"": ""CampaignChangeEvent""
        },
        {
            ""name"": ""CampaignFeed""
        },
        {
            ""name"": ""CampaignHistory""
        },
        {
            ""name"": ""CampaignMember""
        },
        {
            ""name"": ""CampaignMemberChangeEvent""
        },
        {
            ""name"": ""CampaignMemberStatus""
        },
        {
            ""name"": ""CampaignMemberStatusChangeEvent""
        },
        {
            ""name"": ""CampaignShare""
        },
        {
            ""name"": ""CardPaymentMethod""
        },
        {
            ""name"": ""Case""
        },
        {
            ""name"": ""CaseChangeEvent""
        },
        {
            ""name"": ""CaseComment""
        },
        {
            ""name"": ""CaseContactRole""
        },
        {
            ""name"": ""CaseFeed""
        },
        {
            ""name"": ""CaseHistory""
        },
        {
            ""name"": ""CaseMilestone""
        },
        {
            ""name"": ""CaseRelatedIssue""
        },
        {
            ""name"": ""CaseRelatedIssueChangeEvent""
        },
        {
            ""name"": ""CaseRelatedIssueFeed""
        },
        {
            ""name"": ""CaseRelatedIssueHistory""
        },
        {
            ""name"": ""CaseShare""
        },
        {
            ""name"": ""CaseSolution""
        },
        {
            ""name"": ""CaseStatus""
        },
        {
            ""name"": ""CaseTeamMember""
        },
        {
            ""name"": ""CaseTeamRole""
        },
        {
            ""name"": ""CaseTeamTemplate""
        },
        {
            ""name"": ""CaseTeamTemplateMember""
        },
        {
            ""name"": ""CaseTeamTemplateRecord""
        },
        {
            ""name"": ""CategoryData""
        },
        {
            ""name"": ""CategoryNode""
        },
        {
            ""name"": ""ChangeRequest""
        },
        {
            ""name"": ""ChangeRequestChangeEvent""
        },
        {
            ""name"": ""ChangeRequestFeed""
        },
        {
            ""name"": ""ChangeRequestHistory""
        },
        {
            ""name"": ""ChangeRequestRelatedIssue""
        },
        {
            ""name"": ""ChangeRequestRelatedIssueChangeEvent""
        },
        {
            ""name"": ""ChangeRequestRelatedIssueFeed""
        },
        {
            ""name"": ""ChangeRequestRelatedIssueHistory""
        },
        {
            ""name"": ""ChangeRequestRelatedItem""
        },
        {
            ""name"": ""ChangeRequestRelatedItemChangeEvent""
        },
        {
            ""name"": ""ChangeRequestRelatedItemFeed""
        },
        {
            ""name"": ""ChangeRequestRelatedItemHistory""
        },
        {
            ""name"": ""ChangeRequestShare""
        },
        {
            ""name"": ""ChannelProgram""
        },
        {
            ""name"": ""ChannelProgramFeed""
        },
        {
            ""name"": ""ChannelProgramHistory""
        },
        {
            ""name"": ""ChannelProgramLevel""
        },
        {
            ""name"": ""ChannelProgramLevelFeed""
        },
        {
            ""name"": ""ChannelProgramLevelHistory""
        },
        {
            ""name"": ""ChannelProgramLevelShare""
        },
        {
            ""name"": ""ChannelProgramMember""
        },
        {
            ""name"": ""ChannelProgramMemberFeed""
        },
        {
            ""name"": ""ChannelProgramMemberHistory""
        },
        {
            ""name"": ""ChannelProgramMemberShare""
        },
        {
            ""name"": ""ChannelProgramShare""
        },
        {
            ""name"": ""ChatterActivity""
        },
        {
            ""name"": ""ChatterExtension""
        },
        {
            ""name"": ""ChatterExtensionConfig""
        },
        {
            ""name"": ""ClientBrowser""
        },
        {
            ""name"": ""CollaborationGroup""
        },
        {
            ""name"": ""CollaborationGroupFeed""
        },
        {
            ""name"": ""CollaborationGroupMember""
        },
        {
            ""name"": ""CollaborationGroupMemberRequest""
        },
        {
            ""name"": ""CollaborationInvitation""
        },
        {
            ""name"": ""ColorDefinition""
        },
        {
            ""name"": ""CombinedAttachment""
        },
        {
            ""name"": ""CommSubscription""
        },
        {
            ""name"": ""CommSubscriptionChannelType""
        },
        {
            ""name"": ""CommSubscriptionChannelTypeFeed""
        },
        {
            ""name"": ""CommSubscriptionChannelTypeHistory""
        },
        {
            ""name"": ""CommSubscriptionChannelTypeShare""
        },
        {
            ""name"": ""CommSubscriptionConsent""
        },
        {
            ""name"": ""CommSubscriptionConsentChangeEvent""
        },
        {
            ""name"": ""CommSubscriptionConsentFeed""
        },
        {
            ""name"": ""CommSubscriptionConsentHistory""
        },
        {
            ""name"": ""CommSubscriptionConsentShare""
        },
        {
            ""name"": ""CommSubscriptionFeed""
        },
        {
            ""name"": ""CommSubscriptionHistory""
        },
        {
            ""name"": ""CommSubscriptionShare""
        },
        {
            ""name"": ""CommSubscriptionTiming""
        },
        {
            ""name"": ""CommSubscriptionTimingFeed""
        },
        {
            ""name"": ""CommSubscriptionTimingHistory""
        },
        {
            ""name"": ""Community""
        },
        {
            ""name"": ""Competitors__ChangeEvent""
        },
        {
            ""name"": ""Competitors__History""
        },
        {
            ""name"": ""Competitors__c""
        },
        {
            ""name"": ""ConcurLongRunApexErrEvent""
        },
        {
            ""name"": ""ConferenceNumber""
        },
        {
            ""name"": ""ConnectedApplication""
        },
        {
            ""name"": ""ConsumptionRate""
        },
        {
            ""name"": ""ConsumptionRateHistory""
        },
        {
            ""name"": ""ConsumptionSchedule""
        },
        {
            ""name"": ""ConsumptionScheduleFeed""
        },
        {
            ""name"": ""ConsumptionScheduleHistory""
        },
        {
            ""name"": ""ConsumptionScheduleShare""
        },
        {
            ""name"": ""Contact""
        },
        {
            ""name"": ""ContactChangeEvent""
        },
        {
            ""name"": ""ContactFeed""
        },
        {
            ""name"": ""ContactHistory""
        },
        {
            ""name"": ""ContactPointAddress""
        },
        {
            ""name"": ""ContactPointAddressChangeEvent""
        },
        {
            ""name"": ""ContactPointAddressHistory""
        },
        {
            ""name"": ""ContactPointAddressShare""
        },
        {
            ""name"": ""ContactPointConsent""
        },
        {
            ""name"": ""ContactPointConsentChangeEvent""
        },
        {
            ""name"": ""ContactPointConsentHistory""
        },
        {
            ""name"": ""ContactPointConsentShare""
        },
        {
            ""name"": ""ContactPointEmail""
        },
        {
            ""name"": ""ContactPointEmailChangeEvent""
        },
        {
            ""name"": ""ContactPointEmailHistory""
        },
        {
            ""name"": ""ContactPointEmailShare""
        },
        {
            ""name"": ""ContactPointPhone""
        },
        {
            ""name"": ""ContactPointPhoneChangeEvent""
        },
        {
            ""name"": ""ContactPointPhoneHistory""
        },
        {
            ""name"": ""ContactPointPhoneShare""
        },
        {
            ""name"": ""ContactPointTypeConsent""
        },
        {
            ""name"": ""ContactPointTypeConsentChangeEvent""
        },
        {
            ""name"": ""ContactPointTypeConsentHistory""
        },
        {
            ""name"": ""ContactPointTypeConsentShare""
        },
        {
            ""name"": ""ContactRequest""
        },
        {
            ""name"": ""ContactRequestShare""
        },
        {
            ""name"": ""ContactShare""
        },
        {
            ""name"": ""ContentAsset""
        },
        {
            ""name"": ""ContentBody""
        },
        {
            ""name"": ""ContentDistribution""
        },
        {
            ""name"": ""ContentDistributionView""
        },
        {
            ""name"": ""ContentDocument""
        },
        {
            ""name"": ""ContentDocumentChangeEvent""
        },
        {
            ""name"": ""ContentDocumentFeed""
        },
        {
            ""name"": ""ContentDocumentHistory""
        },
        {
            ""name"": ""ContentDocumentLink""
        },
        {
            ""name"": ""ContentDocumentLinkChangeEvent""
        },
        {
            ""name"": ""ContentDocumentSubscription""
        },
        {
            ""name"": ""ContentFolder""
        },
        {
            ""name"": ""ContentFolderItem""
        },
        {
            ""name"": ""ContentFolderLink""
        },
        {
            ""name"": ""ContentFolderMember""
        },
        {
            ""name"": ""ContentNotification""
        },
        {
            ""name"": ""ContentTagSubscription""
        },
        {
            ""name"": ""ContentUserSubscription""
        },
        {
            ""name"": ""ContentVersion""
        },
        {
            ""name"": ""ContentVersionChangeEvent""
        },
        {
            ""name"": ""ContentVersionComment""
        },
        {
            ""name"": ""ContentVersionHistory""
        },
        {
            ""name"": ""ContentVersionRating""
        },
        {
            ""name"": ""ContentWorkspace""
        },
        {
            ""name"": ""ContentWorkspaceDoc""
        },
        {
            ""name"": ""ContentWorkspaceMember""
        },
        {
            ""name"": ""ContentWorkspacePermission""
        },
        {
            ""name"": ""ContentWorkspaceSubscription""
        },
        {
            ""name"": ""ContextParamMap""
        },
        {
            ""name"": ""Contract""
        },
        {
            ""name"": ""ContractChangeEvent""
        },
        {
            ""name"": ""ContractContactRole""
        },
        {
            ""name"": ""ContractFeed""
        },
        {
            ""name"": ""ContractHistory""
        },
        {
            ""name"": ""ContractLineItem""
        },
        {
            ""name"": ""ContractLineItemChangeEvent""
        },
        {
            ""name"": ""ContractLineItemFeed""
        },
        {
            ""name"": ""ContractLineItemHistory""
        },
        {
            ""name"": ""ContractStatus""
        },
        {
            ""name"": ""Conversation""
        },
        {
            ""name"": ""ConversationEntry""
        },
        {
            ""name"": ""ConversationParticipant""
        },
        {
            ""name"": ""CorsWhitelistEntry""
        },
        {
            ""name"": ""CredentialStuffingEvent""
        },
        {
            ""name"": ""CredentialStuffingEventStore""
        },
        {
            ""name"": ""CredentialStuffingEventStoreFeed""
        },
        {
            ""name"": ""CreditMemo""
        },
        {
            ""name"": ""CreditMemoFeed""
        },
        {
            ""name"": ""CreditMemoHistory""
        },
        {
            ""name"": ""CreditMemoInvApplication""
        },
        {
            ""name"": ""CreditMemoInvApplicationFeed""
        },
        {
            ""name"": ""CreditMemoInvApplicationHistory""
        },
        {
            ""name"": ""CreditMemoLine""
        },
        {
            ""name"": ""CreditMemoLineFeed""
        },
        {
            ""name"": ""CreditMemoLineHistory""
        },
        {
            ""name"": ""CreditMemoShare""
        },
        {
            ""name"": ""CronJobDetail""
        },
        {
            ""name"": ""CronTrigger""
        },
        {
            ""name"": ""CspTrustedSite""
        },
        {
            ""name"": ""CustomBrand""
        },
        {
            ""name"": ""CustomBrandAsset""
        },
        {
            ""name"": ""CustomHelpMenuItem""
        },
        {
            ""name"": ""CustomHelpMenuSection""
        },
        {
            ""name"": ""CustomHttpHeader""
        },
        {
            ""name"": ""CustomNotificationType""
        },
        {
            ""name"": ""CustomObjectUserLicenseMetrics""
        },
        {
            ""name"": ""CustomPermission""
        },
        {
            ""name"": ""CustomPermissionDependency""
        },
        {
            ""name"": ""Customer""
        },
        {
            ""name"": ""CustomerShare""
        },
        {
            ""name"": ""DandBCompany""
        },
        {
            ""name"": ""Dashboard""
        },
        {
            ""name"": ""DashboardComponent""
        },
        {
            ""name"": ""DashboardComponentFeed""
        },
        {
            ""name"": ""DashboardFeed""
        },
        {
            ""name"": ""DataAssessmentFieldMetric""
        },
        {
            ""name"": ""DataAssessmentMetric""
        },
        {
            ""name"": ""DataAssessmentValueMetric""
        },
        {
            ""name"": ""DataObjectDataChgEvent""
        },
        {
            ""name"": ""DataStatistics""
        },
        {
            ""name"": ""DataType""
        },
        {
            ""name"": ""DataUseLegalBasis""
        },
        {
            ""name"": ""DataUseLegalBasisHistory""
        },
        {
            ""name"": ""DataUseLegalBasisShare""
        },
        {
            ""name"": ""DataUsePurpose""
        },
        {
            ""name"": ""DataUsePurposeHistory""
        },
        {
            ""name"": ""DataUsePurposeShare""
        },
        {
            ""name"": ""DatacloudAddress""
        },
        {
            ""name"": ""DatacloudDandBCompany""
        },
        {
            ""name"": ""DatacloudOwnedEntity""
        },
        {
            ""name"": ""DatacloudPurchaseUsage""
        },
        {
            ""name"": ""DecisionMatrixDefinition""
        },
        {
            ""name"": ""DecisionMatrixDefinitionVersion""
        },
        {
            ""name"": ""DecisionTable""
        },
        {
            ""name"": ""DecisionTableDatasetLink""
        },
        {
            ""name"": ""DecisionTableParameter""
        },
        {
            ""name"": ""DecisionTableSourceCriteria""
        },
        {
            ""name"": ""DecisionTblDatasetParameter""
        },
        {
            ""name"": ""DeclinedEventRelation""
        },
        {
            ""name"": ""DelegatedAccount""
        },
        {
            ""name"": ""DelegatedAccountFeed""
        },
        {
            ""name"": ""DelegatedAccountHistory""
        },
        {
            ""name"": ""DelegatedAccountShare""
        },
        {
            ""name"": ""DeleteEvent""
        },
        {
            ""name"": ""DigitalSignature""
        },
        {
            ""name"": ""DigitalSignatureChangeEvent""
        },
        {
            ""name"": ""DigitalWallet""
        },
        {
            ""name"": ""Document""
        },
        {
            ""name"": ""DocumentAttachmentMap""
        },
        {
            ""name"": ""DocumentChecklistItem""
        },
        {
            ""name"": ""DocumentChecklistItemFeed""
        },
        {
            ""name"": ""DocumentChecklistItemHistory""
        },
        {
            ""name"": ""DocumentChecklistItemShare""
        },
        {
            ""name"": ""Domain""
        },
        {
            ""name"": ""DomainSite""
        },
        {
            ""name"": ""DuplicateJob""
        },
        {
            ""name"": ""DuplicateJobDefinition""
        },
        {
            ""name"": ""DuplicateJobMatchingRule""
        },
        {
            ""name"": ""DuplicateJobMatchingRuleDefinition""
        },
        {
            ""name"": ""DuplicateRecordItem""
        },
        {
            ""name"": ""DuplicateRecordSet""
        },
        {
            ""name"": ""DuplicateRule""
        },
        {
            ""name"": ""EmailCapture""
        },
        {
            ""name"": ""EmailDomainFilter""
        },
        {
            ""name"": ""EmailDomainKey""
        },
        {
            ""name"": ""EmailMessage""
        },
        {
            ""name"": ""EmailMessageChangeEvent""
        },
        {
            ""name"": ""EmailMessageRelation""
        },
        {
            ""name"": ""EmailRelay""
        },
        {
            ""name"": ""EmailServicesAddress""
        },
        {
            ""name"": ""EmailServicesFunction""
        },
        {
            ""name"": ""EmailStatus""
        },
        {
            ""name"": ""EmailTemplate""
        },
        {
            ""name"": ""EmailTemplateChangeEvent""
        },
        {
            ""name"": ""EmbeddedServiceDetail""
        },
        {
            ""name"": ""EmbeddedServiceLabel""
        },
        {
            ""name"": ""EngagementChannelType""
        },
        {
            ""name"": ""EngagementChannelTypeFeed""
        },
        {
            ""name"": ""EngagementChannelTypeHistory""
        },
        {
            ""name"": ""EngagementChannelTypeShare""
        },
        {
            ""name"": ""EnhancedLetterhead""
        },
        {
            ""name"": ""EnhancedLetterheadFeed""
        },
        {
            ""name"": ""Entitlement""
        },
        {
            ""name"": ""EntitlementChangeEvent""
        },
        {
            ""name"": ""EntitlementContact""
        },
        {
            ""name"": ""EntitlementFeed""
        },
        {
            ""name"": ""EntitlementHistory""
        },
        {
            ""name"": ""EntitlementTemplate""
        },
        {
            ""name"": ""EntityDefinition""
        },
        {
            ""name"": ""EntityMilestone""
        },
        {
            ""name"": ""EntityMilestoneFeed""
        },
        {
            ""name"": ""EntityMilestoneHistory""
        },
        {
            ""name"": ""EntityParticle""
        },
        {
            ""name"": ""EntitySubscription""
        },
        {
            ""name"": ""Event""
        },
        {
            ""name"": ""EventBusSubscriber""
        },
        {
            ""name"": ""EventChangeEvent""
        },
        {
            ""name"": ""EventFeed""
        },
        {
            ""name"": ""EventLogFile""
        },
        {
            ""name"": ""EventRelation""
        },
        {
            ""name"": ""EventRelationChangeEvent""
        },
        {
            ""name"": ""EventRelayConfig""
        },
        {
            ""name"": ""EventRelayConfigChangeEvent""
        },
        {
            ""name"": ""EventRelayFeedback""
        },
        {
            ""name"": ""ExperienceDiagnosticEvent""
        },
        {
            ""name"": ""ExpressionFilter""
        },
        {
            ""name"": ""ExpressionFilterCriteria""
        },
        {
            ""name"": ""ExpressionSet""
        },
        {
            ""name"": ""ExpressionSetDefinition""
        },
        {
            ""name"": ""ExpressionSetDefinitionVersion""
        },
        {
            ""name"": ""ExpressionSetFeed""
        },
        {
            ""name"": ""ExpressionSetHistory""
        },
        {
            ""name"": ""ExpressionSetObjectAlias""
        },
        {
            ""name"": ""ExpressionSetShare""
        },
        {
            ""name"": ""ExpressionSetVersion""
        },
        {
            ""name"": ""ExpressionSetVersionFeed""
        },
        {
            ""name"": ""ExpressionSetVersionHistory""
        },
        {
            ""name"": ""ExpressionSetView""
        },
        {
            ""name"": ""ExpsSetObjectAliasFieldVw""
        },
        {
            ""name"": ""ExternalDataSource""
        },
        {
            ""name"": ""ExternalDataSrcDescriptor""
        },
        {
            ""name"": ""ExternalDataUserAuth""
        },
        {
            ""name"": ""ExternalEvent""
        },
        {
            ""name"": ""ExternalEventMapping""
        },
        {
            ""name"": ""ExternalEventMappingShare""
        },
        {
            ""name"": ""FeedAttachment""
        },
        {
            ""name"": ""FeedComment""
        },
        {
            ""name"": ""FeedItem""
        },
        {
            ""name"": ""FeedLike""
        },
        {
            ""name"": ""FeedPollChoice""
        },
        {
            ""name"": ""FeedPollVote""
        },
        {
            ""name"": ""FeedRevision""
        },
        {
            ""name"": ""FeedSignal""
        },
        {
            ""name"": ""FeedTrackedChange""
        },
        {
            ""name"": ""FieldDefinition""
        },
        {
            ""name"": ""FieldHistoryArchive""
        },
        {
            ""name"": ""FieldPermissions""
        },
        {
            ""name"": ""FieldSecurityClassification""
        },
        {
            ""name"": ""FileEvent""
        },
        {
            ""name"": ""FileEventStore""
        },
        {
            ""name"": ""FileSearchActivity""
        },
        {
            ""name"": ""FinanceBalanceSnapshot""
        },
        {
            ""name"": ""FinanceBalanceSnapshotChangeEvent""
        },
        {
            ""name"": ""FinanceBalanceSnapshotShare""
        },
        {
            ""name"": ""FinanceTransaction""
        },
        {
            ""name"": ""FinanceTransactionChangeEvent""
        },
        {
            ""name"": ""FinanceTransactionShare""
        },
        {
            ""name"": ""FiscalYearSettings""
        },
        {
            ""name"": ""FlexQueueItem""
        },
        {
            ""name"": ""FlowDefinitionView""
        },
        {
            ""name"": ""FlowExecutionErrorEvent""
        },
        {
            ""name"": ""FlowInterview""
        },
        {
            ""name"": ""FlowInterviewLog""
        },
        {
            ""name"": ""FlowInterviewLogEntry""
        },
        {
            ""name"": ""FlowInterviewLogShare""
        },
        {
            ""name"": ""FlowInterviewShare""
        },
        {
            ""name"": ""FlowOrchestrationEvent""
        },
        {
            ""name"": ""FlowOrchestrationInstance""
        },
        {
            ""name"": ""FlowOrchestrationInstanceShare""
        },
        {
            ""name"": ""FlowOrchestrationStageInstance""
        },
        {
            ""name"": ""FlowOrchestrationStageInstanceShare""
        },
        {
            ""name"": ""FlowOrchestrationStepInstance""
        },
        {
            ""name"": ""FlowOrchestrationStepInstanceShare""
        },
        {
            ""name"": ""FlowOrchestrationWorkItem""
        },
        {
            ""name"": ""FlowOrchestrationWorkItemShare""
        },
        {
            ""name"": ""FlowRecordRelation""
        },
        {
            ""name"": ""FlowStageRelation""
        },
        {
            ""name"": ""FlowTestResult""
        },
        {
            ""name"": ""FlowTestResultShare""
        },
        {
            ""name"": ""FlowTestView""
        },
        {
            ""name"": ""FlowVariableView""
        },
        {
            ""name"": ""FlowVersionView""
        },
        {
            ""name"": ""Folder""
        },
        {
            ""name"": ""FolderedContentDocument""
        },
        {
            ""name"": ""FormulaFunction""
        },
        {
            ""name"": ""FormulaFunctionAllowedType""
        },
        {
            ""name"": ""FormulaFunctionCategory""
        },
        {
            ""name"": ""GrantedByLicense""
        },
        {
            ""name"": ""Group""
        },
        {
            ""name"": ""GroupMember""
        },
        {
            ""name"": ""GtwyProvPaymentMethodType""
        },
        {
            ""name"": ""Holiday""
        },
        {
            ""name"": ""IPAddressRange""
        },
        {
            ""name"": ""IconDefinition""
        },
        {
            ""name"": ""Idea""
        },
        {
            ""name"": ""IdeaComment""
        },
        {
            ""name"": ""IdentityProviderEventStore""
        },
        {
            ""name"": ""IdentityVerificationEvent""
        },
        {
            ""name"": ""IdpEventLog""
        },
        {
            ""name"": ""IframeWhiteListUrl""
        },
        {
            ""name"": ""Image""
        },
        {
            ""name"": ""ImageFeed""
        },
        {
            ""name"": ""ImageHistory""
        },
        {
            ""name"": ""ImageShare""
        },
        {
            ""name"": ""Incident""
        },
        {
            ""name"": ""IncidentChangeEvent""
        },
        {
            ""name"": ""IncidentFeed""
        },
        {
            ""name"": ""IncidentHistory""
        },
        {
            ""name"": ""IncidentRelatedItem""
        },
        {
            ""name"": ""IncidentRelatedItemChangeEvent""
        },
        {
            ""name"": ""IncidentRelatedItemFeed""
        },
        {
            ""name"": ""IncidentRelatedItemHistory""
        },
        {
            ""name"": ""IncidentShare""
        },
        {
            ""name"": ""Individual""
        },
        {
            ""name"": ""IndividualChangeEvent""
        },
        {
            ""name"": ""IndividualHistory""
        },
        {
            ""name"": ""IndividualShare""
        },
        {
            ""name"": ""InstalledMobileApp""
        },
        {
            ""name"": ""Invoice""
        },
        {
            ""name"": ""InvoiceFeed""
        },
        {
            ""name"": ""InvoiceHistory""
        },
        {
            ""name"": ""InvoiceLine""
        },
        {
            ""name"": ""InvoiceLineFeed""
        },
        {
            ""name"": ""InvoiceLineHistory""
        },
        {
            ""name"": ""InvoiceShare""
        },
        {
            ""name"": ""JournalType""
        },
        {
            ""name"": ""JournalTypeHistory""
        },
        {
            ""name"": ""JournalTypeShare""
        },
        {
            ""name"": ""KnowledgeableUser""
        },
        {
            ""name"": ""Lead""
        },
        {
            ""name"": ""LeadChangeEvent""
        },
        {
            ""name"": ""LeadFeed""
        },
        {
            ""name"": ""LeadHistory""
        },
        {
            ""name"": ""LeadShare""
        },
        {
            ""name"": ""LeadStatus""
        },
        {
            ""name"": ""LegalEntity""
        },
        {
            ""name"": ""LegalEntityFeed""
        },
        {
            ""name"": ""LegalEntityHistory""
        },
        {
            ""name"": ""LegalEntityShare""
        },
        {
            ""name"": ""LightningExitByPageMetrics""
        },
        {
            ""name"": ""LightningExperienceTheme""
        },
        {
            ""name"": ""LightningOnboardingConfig""
        },
        {
            ""name"": ""LightningToggleMetrics""
        },
        {
            ""name"": ""LightningUriEvent""
        },
        {
            ""name"": ""LightningUriEventStream""
        },
        {
            ""name"": ""LightningUsageByAppTypeMetrics""
        },
        {
            ""name"": ""LightningUsageByBrowserMetrics""
        },
        {
            ""name"": ""LightningUsageByFlexiPageMetrics""
        },
        {
            ""name"": ""LightningUsageByPageMetrics""
        },
        {
            ""name"": ""ListEmail""
        },
        {
            ""name"": ""ListEmailChangeEvent""
        },
        {
            ""name"": ""ListEmailIndividualRecipient""
        },
        {
            ""name"": ""ListEmailRecipientSource""
        },
        {
            ""name"": ""ListEmailShare""
        },
        {
            ""name"": ""ListView""
        },
        {
            ""name"": ""ListViewChart""
        },
        {
            ""name"": ""ListViewChartInstance""
        },
        {
            ""name"": ""ListViewEvent""
        },
        {
            ""name"": ""ListViewEventStream""
        },
        {
            ""name"": ""Loads__ChangeEvent""
        },
        {
            ""name"": ""Loads__Share""
        },
        {
            ""name"": ""Loads__c""
        },
        {
            ""name"": ""Location""
        },
        {
            ""name"": ""LocationChangeEvent""
        },
        {
            ""name"": ""LocationFeed""
        },
        {
            ""name"": ""LocationGroup""
        },
        {
            ""name"": ""LocationGroupAssignment""
        },
        {
            ""name"": ""LocationGroupFeed""
        },
        {
            ""name"": ""LocationGroupHistory""
        },
        {
            ""name"": ""LocationGroupShare""
        },
        {
            ""name"": ""LocationHistory""
        },
        {
            ""name"": ""LocationShare""
        },
        {
            ""name"": ""LoginAsEvent""
        },
        {
            ""name"": ""LoginAsEventStream""
        },
        {
            ""name"": ""LoginEvent""
        },
        {
            ""name"": ""LoginEventStream""
        },
        {
            ""name"": ""LoginGeo""
        },
        {
            ""name"": ""LoginHistory""
        },
        {
            ""name"": ""LoginIp""
        },
        {
            ""name"": ""LogoutEvent""
        },
        {
            ""name"": ""LogoutEventStream""
        },
        {
            ""name"": ""LookedUpFromActivity""
        },
        {
            ""name"": ""MLModel""
        },
        {
            ""name"": ""MLModelFactor""
        },
        {
            ""name"": ""MLModelFactorComponent""
        },
        {
            ""name"": ""MLModelMetric""
        },
        {
            ""name"": ""MLPredictionDefinition""
        },
        {
            ""name"": ""MLRecommendationDefinition""
        },
        {
            ""name"": ""Macro""
        },
        {
            ""name"": ""MacroChangeEvent""
        },
        {
            ""name"": ""MacroHistory""
        },
        {
            ""name"": ""MacroInstruction""
        },
        {
            ""name"": ""MacroInstructionChangeEvent""
        },
        {
            ""name"": ""MacroShare""
        },
        {
            ""name"": ""MacroUsage""
        },
        {
            ""name"": ""MacroUsageShare""
        },
        {
            ""name"": ""MailmergeTemplate""
        },
        {
            ""name"": ""ManagedContent""
        },
        {
            ""name"": ""ManagedContentChannel""
        },
        {
            ""name"": ""ManagedContentSpace""
        },
        {
            ""name"": ""ManagedContentVariant""
        },
        {
            ""name"": ""ManagedContentVariantChangeEvent""
        },
        {
            ""name"": ""MatchingInformation""
        },
        {
            ""name"": ""MatchingRule""
        },
        {
            ""name"": ""MatchingRuleItem""
        },
        {
            ""name"": ""MessagingChannel""
        },
        {
            ""name"": ""MessagingEndUser""
        },
        {
            ""name"": ""MessagingEndUserHistory""
        },
        {
            ""name"": ""MessagingEndUserShare""
        },
        {
            ""name"": ""MessagingSession""
        },
        {
            ""name"": ""MessagingSessionFeed""
        },
        {
            ""name"": ""MessagingSessionHistory""
        },
        {
            ""name"": ""MessagingSessionShare""
        },
        {
            ""name"": ""MilestoneType""
        },
        {
            ""name"": ""MlQueryObservation""
        },
        {
            ""name"": ""MobileApplicationDetail""
        },
        {
            ""name"": ""MsgChannelLanguageKeyword""
        },
        {
            ""name"": ""MutingPermissionSet""
        },
        {
            ""name"": ""MyDomainDiscoverableLogin""
        },
        {
            ""name"": ""Name""
        },
        {
            ""name"": ""NamedCredential""
        },
        {
            ""name"": ""NavigationLinkSet""
        },
        {
            ""name"": ""NavigationMenuItem""
        },
        {
            ""name"": ""Network""
        },
        {
            ""name"": ""NetworkActivityAudit""
        },
        {
            ""name"": ""NetworkAffinity""
        },
        {
            ""name"": ""NetworkAuthApiSettings""
        },
        {
            ""name"": ""NetworkDiscoverableLogin""
        },
        {
            ""name"": ""NetworkFeedResponseMetric""
        },
        {
            ""name"": ""NetworkMember""
        },
        {
            ""name"": ""NetworkMemberGroup""
        },
        {
            ""name"": ""NetworkModeration""
        },
        {
            ""name"": ""NetworkPageOverride""
        },
        {
            ""name"": ""NetworkSelfRegistration""
        },
        {
            ""name"": ""NetworkUserHistoryRecent""
        },
        {
            ""name"": ""Note""
        },
        {
            ""name"": ""NoteAndAttachment""
        },
        {
            ""name"": ""OauthCustomScope""
        },
        {
            ""name"": ""OauthCustomScopeApp""
        },
        {
            ""name"": ""OauthToken""
        },
        {
            ""name"": ""ObjectPermissions""
        },
        {
            ""name"": ""OmniDataPack""
        },
        {
            ""name"": ""OmniDataPackFeed""
        },
        {
            ""name"": ""OmniDataPackShare""
        },
        {
            ""name"": ""OmniDataTransform""
        },
        {
            ""name"": ""OmniDataTransformConfig""
        },
        {
            ""name"": ""OmniDataTransformItem""
        },
        {
            ""name"": ""OmniDataTransformShare""
        },
        {
            ""name"": ""OmniESignatureTemplate""
        },
        {
            ""name"": ""OmniESignatureTemplateShare""
        },
        {
            ""name"": ""OmniIntegrationProcConfig""
        },
        {
            ""name"": ""OmniInteractionAccessConfig""
        },
        {
            ""name"": ""OmniInteractionConfig""
        },
        {
            ""name"": ""OmniProcess""
        },
        {
            ""name"": ""OmniProcessCompilation""
        },
        {
            ""name"": ""OmniProcessElement""
        },
        {
            ""name"": ""OmniProcessFeed""
        },
        {
            ""name"": ""OmniProcessShare""
        },
        {
            ""name"": ""OmniProcessTransientData""
        },
        {
            ""name"": ""OmniProcessTransientDataFeed""
        },
        {
            ""name"": ""OmniProcessTransientDataShare""
        },
        {
            ""name"": ""OmniScriptConfig""
        },
        {
            ""name"": ""OmniScriptSavedSession""
        },
        {
            ""name"": ""OmniScriptSavedSessionFeed""
        },
        {
            ""name"": ""OmniScriptSavedSessionShare""
        },
        {
            ""name"": ""OmniUiCard""
        },
        {
            ""name"": ""OmniUiCardConfig""
        },
        {
            ""name"": ""OmniUiCardFeed""
        },
        {
            ""name"": ""OmniUiCardShare""
        },
        {
            ""name"": ""OnboardingMetrics""
        },
        {
            ""name"": ""OpenActivity""
        },
        {
            ""name"": ""Opportunity""
        },
        {
            ""name"": ""OpportunityChangeEvent""
        },
        {
            ""name"": ""OpportunityCompetitor""
        },
        {
            ""name"": ""OpportunityContactRole""
        },
        {
            ""name"": ""OpportunityContactRoleChangeEvent""
        },
        {
            ""name"": ""OpportunityFeed""
        },
        {
            ""name"": ""OpportunityFieldHistory""
        },
        {
            ""name"": ""OpportunityHistory""
        },
        {
            ""name"": ""OpportunityLineItem""
        },
        {
            ""name"": ""OpportunityPartner""
        },
        {
            ""name"": ""OpportunityShare""
        },
        {
            ""name"": ""OpportunityStage""
        },
        {
            ""name"": ""Order""
        },
        {
            ""name"": ""OrderChangeEvent""
        },
        {
            ""name"": ""OrderFeed""
        },
        {
            ""name"": ""OrderHistory""
        },
        {
            ""name"": ""OrderItem""
        },
        {
            ""name"": ""OrderItemChangeEvent""
        },
        {
            ""name"": ""OrderItemFeed""
        },
        {
            ""name"": ""OrderItemHistory""
        },
        {
            ""name"": ""OrderShare""
        },
        {
            ""name"": ""OrderStatus""
        },
        {
            ""name"": ""OrgDeleteRequest""
        },
        {
            ""name"": ""OrgDeleteRequestShare""
        },
        {
            ""name"": ""OrgEmailAddressSecurity""
        },
        {
            ""name"": ""OrgLifecycleNotification""
        },
        {
            ""name"": ""OrgMetric""
        },
        {
            ""name"": ""OrgMetricScanResult""
        },
        {
            ""name"": ""OrgMetricScanSummary""
        },
        {
            ""name"": ""OrgWideEmailAddress""
        },
        {
            ""name"": ""Organization""
        },
        {
            ""name"": ""OtherComponentTask""
        },
        {
            ""name"": ""OtherComponentTaskFeed""
        },
        {
            ""name"": ""OtherComponentTaskHistory""
        },
        {
            ""name"": ""OtherComponentTaskShare""
        },
        {
            ""name"": ""OutOfOffice""
        },
        {
            ""name"": ""OutgoingEmail""
        },
        {
            ""name"": ""OutgoingEmailRelation""
        },
        {
            ""name"": ""OwnedContentDocument""
        },
        {
            ""name"": ""OwnerChangeOptionInfo""
        },
        {
            ""name"": ""PackageLicense""
        },
        {
            ""name"": ""Participant""
        },
        {
            ""name"": ""Partner""
        },
        {
            ""name"": ""PartnerFundAllocation""
        },
        {
            ""name"": ""PartnerFundAllocationFeed""
        },
        {
            ""name"": ""PartnerFundAllocationHistory""
        },
        {
            ""name"": ""PartnerFundAllocationShare""
        },
        {
            ""name"": ""PartnerFundClaim""
        },
        {
            ""name"": ""PartnerFundClaimFeed""
        },
        {
            ""name"": ""PartnerFundClaimHistory""
        },
        {
            ""name"": ""PartnerFundClaimShare""
        },
        {
            ""name"": ""PartnerFundRequest""
        },
        {
            ""name"": ""PartnerFundRequestFeed""
        },
        {
            ""name"": ""PartnerFundRequestHistory""
        },
        {
            ""name"": ""PartnerFundRequestShare""
        },
        {
            ""name"": ""PartnerMarketingBudget""
        },
        {
            ""name"": ""PartnerMarketingBudgetFeed""
        },
        {
            ""name"": ""PartnerMarketingBudgetHistory""
        },
        {
            ""name"": ""PartnerMarketingBudgetShare""
        },
        {
            ""name"": ""PartnerRole""
        },
        {
            ""name"": ""PartyConsent""
        },
        {
            ""name"": ""PartyConsentChangeEvent""
        },
        {
            ""name"": ""PartyConsentFeed""
        },
        {
            ""name"": ""PartyConsentHistory""
        },
        {
            ""name"": ""PartyConsentShare""
        },
        {
            ""name"": ""Payment""
        },
        {
            ""name"": ""PaymentAuthAdjustment""
        },
        {
            ""name"": ""PaymentAuthorization""
        },
        {
            ""name"": ""PaymentFeed""
        },
        {
            ""name"": ""PaymentGateway""
        },
        {
            ""name"": ""PaymentGatewayLog""
        },
        {
            ""name"": ""PaymentGatewayProvider""
        },
        {
            ""name"": ""PaymentGroup""
        },
        {
            ""name"": ""PaymentLineInvoice""
        },
        {
            ""name"": ""PaymentMethod""
        },
        {
            ""name"": ""Period""
        },
        {
            ""name"": ""PermissionSet""
        },
        {
            ""name"": ""PermissionSetAssignment""
        },
        {
            ""name"": ""PermissionSetEvent""
        },
        {
            ""name"": ""PermissionSetEventStore""
        },
        {
            ""name"": ""PermissionSetGroup""
        },
        {
            ""name"": ""PermissionSetGroupComponent""
        },
        {
            ""name"": ""PermissionSetLicense""
        },
        {
            ""name"": ""PermissionSetLicenseAssign""
        },
        {
            ""name"": ""PermissionSetTabSetting""
        },
        {
            ""name"": ""PersonAccountOwnerPowerUser""
        },
        {
            ""name"": ""PersonalizationTargetInfo""
        },
        {
            ""name"": ""PicklistValueInfo""
        },
        {
            ""name"": ""PipelineInspectionListView""
        },
        {
            ""name"": ""PlatformAction""
        },
        {
            ""name"": ""PlatformCachePartition""
        },
        {
            ""name"": ""PlatformCachePartitionType""
        },
        {
            ""name"": ""PlatformEventUsageMetric""
        },
        {
            ""name"": ""PlatformStatusAlertEvent""
        },
        {
            ""name"": ""PortalDelegablePermissionSet""
        },
        {
            ""name"": ""Pricebook2""
        },
        {
            ""name"": ""Pricebook2ChangeEvent""
        },
        {
            ""name"": ""Pricebook2History""
        },
        {
            ""name"": ""PricebookEntry""
        },
        {
            ""name"": ""PricebookEntryChangeEvent""
        },
        {
            ""name"": ""PricebookEntryHistory""
        },
        {
            ""name"": ""Problem""
        },
        {
            ""name"": ""ProblemChangeEvent""
        },
        {
            ""name"": ""ProblemFeed""
        },
        {
            ""name"": ""ProblemHistory""
        },
        {
            ""name"": ""ProblemIncident""
        },
        {
            ""name"": ""ProblemIncidentChangeEvent""
        },
        {
            ""name"": ""ProblemIncidentFeed""
        },
        {
            ""name"": ""ProblemIncidentHistory""
        },
        {
            ""name"": ""ProblemRelatedItem""
        },
        {
            ""name"": ""ProblemRelatedItemChangeEvent""
        },
        {
            ""name"": ""ProblemRelatedItemFeed""
        },
        {
            ""name"": ""ProblemRelatedItemHistory""
        },
        {
            ""name"": ""ProblemShare""
        },
        {
            ""name"": ""ProcessDefinition""
        },
        {
            ""name"": ""ProcessFlowMigration""
        },
        {
            ""name"": ""ProcessInstance""
        },
        {
            ""name"": ""ProcessInstanceHistory""
        },
        {
            ""name"": ""ProcessInstanceNode""
        },
        {
            ""name"": ""ProcessInstanceStep""
        },
        {
            ""name"": ""ProcessInstanceWorkitem""
        },
        {
            ""name"": ""ProcessNode""
        },
        {
            ""name"": ""Product2""
        },
        {
            ""name"": ""Product2ChangeEvent""
        },
        {
            ""name"": ""Product2Feed""
        },
        {
            ""name"": ""Product2History""
        },
        {
            ""name"": ""ProductAvailabilityProjection""
        },
        {
            ""name"": ""ProductAvailabilityProjectionFeed""
        },
        {
            ""name"": ""ProductAvailabilityProjectionHistory""
        },
        {
            ""name"": ""ProductAvailabilityProjectionShare""
        },
        {
            ""name"": ""ProductConsumptionSchedule""
        },
        {
            ""name"": ""ProductEntitlementTemplate""
        },
        {
            ""name"": ""ProductFulfillmentLocation""
        },
        {
            ""name"": ""ProductFulfillmentLocationFeed""
        },
        {
            ""name"": ""ProductFulfillmentLocationHistory""
        },
        {
            ""name"": ""ProductFulfillmentLocationShare""
        },
        {
            ""name"": ""ProductItem""
        },
        {
            ""name"": ""ProductItemChangeEvent""
        },
        {
            ""name"": ""ProductItemFeed""
        },
        {
            ""name"": ""ProductItemHistory""
        },
        {
            ""name"": ""ProductItemShare""
        },
        {
            ""name"": ""ProductItemTransaction""
        },
        {
            ""name"": ""ProductItemTransactionFeed""
        },
        {
            ""name"": ""ProductItemTransactionHistory""
        },
        {
            ""name"": ""ProductRequired""
        },
        {
            ""name"": ""ProductRequiredChangeEvent""
        },
        {
            ""name"": ""ProductRequiredFeed""
        },
        {
            ""name"": ""ProductRequiredHistory""
        },
        {
            ""name"": ""ProductTransfer""
        },
        {
            ""name"": ""ProductTransferChangeEvent""
        },
        {
            ""name"": ""ProductTransferFeed""
        },
        {
            ""name"": ""ProductTransferHistory""
        },
        {
            ""name"": ""ProductTransferShare""
        },
        {
            ""name"": ""Profile""
        },
        {
            ""name"": ""Prompt""
        },
        {
            ""name"": ""PromptAction""
        },
        {
            ""name"": ""PromptActionShare""
        },
        {
            ""name"": ""PromptError""
        },
        {
            ""name"": ""PromptErrorShare""
        },
        {
            ""name"": ""PromptVersion""
        },
        {
            ""name"": ""Publisher""
        },
        {
            ""name"": ""PushTopic""
        },
        {
            ""name"": ""PushUpgradeExcludedOrg""
        },
        {
            ""name"": ""QueueSobject""
        },
        {
            ""name"": ""QuickText""
        },
        {
            ""name"": ""QuickTextChangeEvent""
        },
        {
            ""name"": ""QuickTextHistory""
        },
        {
            ""name"": ""QuickTextShare""
        },
        {
            ""name"": ""QuickTextUsage""
        },
        {
            ""name"": ""QuickTextUsageShare""
        },
        {
            ""name"": ""Quote""
        },
        {
            ""name"": ""QuoteChangeEvent""
        },
        {
            ""name"": ""QuoteDocument""
        },
        {
            ""name"": ""QuoteFeed""
        },
        {
            ""name"": ""QuoteHistory""
        },
        {
            ""name"": ""QuoteLineItem""
        },
        {
            ""name"": ""QuoteLineItemChangeEvent""
        },
        {
            ""name"": ""QuoteLineItemHistory""
        },
        {
            ""name"": ""QuoteShare""
        },
        {
            ""name"": ""QuoteTemplateRichTextData""
        },
        {
            ""name"": ""ReceivedDocument""
        },
        {
            ""name"": ""ReceivedDocumentFeed""
        },
        {
            ""name"": ""ReceivedDocumentHistory""
        },
        {
            ""name"": ""ReceivedDocumentShare""
        },
        {
            ""name"": ""ReceivedDocumentType""
        },
        {
            ""name"": ""ReceivedDocumentTypeFeed""
        },
        {
            ""name"": ""ReceivedDocumentTypeHistory""
        },
        {
            ""name"": ""RecentlyViewed""
        },
        {
            ""name"": ""Recommendation""
        },
        {
            ""name"": ""RecommendationChangeEvent""
        },
        {
            ""name"": ""RecommendationResponse""
        },
        {
            ""name"": ""RecordAction""
        },
        {
            ""name"": ""RecordActionHistory""
        },
        {
            ""name"": ""RecordType""
        },
        {
            ""name"": ""RecurrenceSchedule""
        },
        {
            ""name"": ""RecurrenceScheduleShare""
        },
        {
            ""name"": ""RedirectWhitelistUrl""
        },
        {
            ""name"": ""Refund""
        },
        {
            ""name"": ""RefundLinePayment""
        },
        {
            ""name"": ""RelatedListColumnDefinition""
        },
        {
            ""name"": ""RelatedListDefinition""
        },
        {
            ""name"": ""RelationshipDomain""
        },
        {
            ""name"": ""RelationshipInfo""
        },
        {
            ""name"": ""Report""
        },
        {
            ""name"": ""ReportAnomalyEvent""
        },
        {
            ""name"": ""ReportAnomalyEventStore""
        },
        {
            ""name"": ""ReportAnomalyEventStoreFeed""
        },
        {
            ""name"": ""ReportEvent""
        },
        {
            ""name"": ""ReportEventStream""
        },
        {
            ""name"": ""ReportFeed""
        },
        {
            ""name"": ""ReputationLevel""
        },
        {
            ""name"": ""ReputationPointsRule""
        },
        {
            ""name"": ""RevenueAsyncOperation""
        },
        {
            ""name"": ""RevenueTransactionErrorLog""
        },
        {
            ""name"": ""RevenueTransactionErrorLogShare""
        },
        {
            ""name"": ""SOSDeployment""
        },
        {
            ""name"": ""SOSSession""
        },
        {
            ""name"": ""SOSSessionActivity""
        },
        {
            ""name"": ""SOSSessionFeed""
        },
        {
            ""name"": ""SOSSessionHistory""
        },
        {
            ""name"": ""SOSSessionShare""
        },
        {
            ""name"": ""SPSamlAttributes""
        },
        {
            ""name"": ""SamlSsoConfig""
        },
        {
            ""name"": ""Scontrol""
        },
        {
            ""name"": ""Scorecard""
        },
        {
            ""name"": ""ScorecardAssociation""
        },
        {
            ""name"": ""ScorecardMetric""
        },
        {
            ""name"": ""ScorecardShare""
        },
        {
            ""name"": ""SearchLayout""
        },
        {
            ""name"": ""SearchPromotionRule""
        },
        {
            ""name"": ""SecurityCustomBaseline""
        },
        {
            ""name"": ""Seller""
        },
        {
            ""name"": ""SellerHistory""
        },
        {
            ""name"": ""SellerShare""
        },
        {
            ""name"": ""ServiceContract""
        },
        {
            ""name"": ""ServiceContractChangeEvent""
        },
        {
            ""name"": ""ServiceContractFeed""
        },
        {
            ""name"": ""ServiceContractHistory""
        },
        {
            ""name"": ""ServiceContractShare""
        },
        {
            ""name"": ""ServiceSetupProvisioning""
        },
        {
            ""name"": ""SessionHijackingEvent""
        },
        {
            ""name"": ""SessionHijackingEventStore""
        },
        {
            ""name"": ""SessionHijackingEventStoreFeed""
        },
        {
            ""name"": ""SessionPermSetActivation""
        },
        {
            ""name"": ""SetupAssistantStep""
        },
        {
            ""name"": ""SetupAuditTrail""
        },
        {
            ""name"": ""SetupEntityAccess""
        },
        {
            ""name"": ""Shipment""
        },
        {
            ""name"": ""ShipmentChangeEvent""
        },
        {
            ""name"": ""ShipmentFeed""
        },
        {
            ""name"": ""ShipmentHistory""
        },
        {
            ""name"": ""ShipmentItem""
        },
        {
            ""name"": ""ShipmentItemFeed""
        },
        {
            ""name"": ""ShipmentItemHistory""
        },
        {
            ""name"": ""ShipmentShare""
        },
        {
            ""name"": ""SignatureTask""
        },
        {
            ""name"": ""SignatureTaskFeed""
        },
        {
            ""name"": ""SignatureTaskHistory""
        },
        {
            ""name"": ""SignatureTaskLineItem""
        },
        {
            ""name"": ""SignatureTaskLineItemFeed""
        },
        {
            ""name"": ""SignatureTaskLineItemHistory""
        },
        {
            ""name"": ""Site""
        },
        {
            ""name"": ""SiteDetail""
        },
        {
            ""name"": ""SiteFeed""
        },
        {
            ""name"": ""SiteHistory""
        },
        {
            ""name"": ""SiteIframeWhiteListUrl""
        },
        {
            ""name"": ""SiteMarketingDataExtensionMapping""
        },
        {
            ""name"": ""SiteRedirectMapping""
        },
        {
            ""name"": ""SlaProcess""
        },
        {
            ""name"": ""Solution""
        },
        {
            ""name"": ""SolutionFeed""
        },
        {
            ""name"": ""SolutionHistory""
        },
        {
            ""name"": ""SolutionStatus""
        },
        {
            ""name"": ""Stamp""
        },
        {
            ""name"": ""StampAssignment""
        },
        {
            ""name"": ""StaticResource""
        },
        {
            ""name"": ""StreamingChannel""
        },
        {
            ""name"": ""StreamingChannelShare""
        },
        {
            ""name"": ""SubscriberPackage""
        },
        {
            ""name"": ""SubscriberPackageVersion""
        },
        {
            ""name"": ""TabDefinition""
        },
        {
            ""name"": ""Task""
        },
        {
            ""name"": ""TaskChangeEvent""
        },
        {
            ""name"": ""TaskFeed""
        },
        {
            ""name"": ""TaskPriority""
        },
        {
            ""name"": ""TaskStatus""
        },
        {
            ""name"": ""TenantUsageEntitlement""
        },
        {
            ""name"": ""TestSuiteMembership""
        },
        {
            ""name"": ""ThirdPartyAccountLink""
        },
        {
            ""name"": ""ThreatDetectionFeedback""
        },
        {
            ""name"": ""ThreatDetectionFeedbackFeed""
        },
        {
            ""name"": ""TodayGoal""
        },
        {
            ""name"": ""TodayGoalShare""
        },
        {
            ""name"": ""Topic""
        },
        {
            ""name"": ""TopicAssignment""
        },
        {
            ""name"": ""TopicFeed""
        },
        {
            ""name"": ""TopicUserEvent""
        },
        {
            ""name"": ""TransactionSecurityPolicy""
        },
        {
            ""name"": ""Translation""
        },
        {
            ""name"": ""UiFormulaCriterion""
        },
        {
            ""name"": ""UiFormulaRule""
        },
        {
            ""name"": ""UndecidedEventRelation""
        },
        {
            ""name"": ""UriEvent""
        },
        {
            ""name"": ""UriEventStream""
        },
        {
            ""name"": ""User""
        },
        {
            ""name"": ""UserAppInfo""
        },
        {
            ""name"": ""UserAppMenuCustomization""
        },
        {
            ""name"": ""UserAppMenuCustomizationShare""
        },
        {
            ""name"": ""UserAppMenuItem""
        },
        {
            ""name"": ""UserChangeEvent""
        },
        {
            ""name"": ""UserCustomBadge""
        },
        {
            ""name"": ""UserEmailPreferredPerson""
        },
        {
            ""name"": ""UserEmailPreferredPersonShare""
        },
        {
            ""name"": ""UserEntityAccess""
        },
        {
            ""name"": ""UserFeed""
        },
        {
            ""name"": ""UserFieldAccess""
        },
        {
            ""name"": ""UserLicense""
        },
        {
            ""name"": ""UserListView""
        },
        {
            ""name"": ""UserListViewCriterion""
        },
        {
            ""name"": ""UserLogin""
        },
        {
            ""name"": ""UserPackageLicense""
        },
        {
            ""name"": ""UserPermissionAccess""
        },
        {
            ""name"": ""UserPreference""
        },
        {
            ""name"": ""UserPrioritizedRecord""
        },
        {
            ""name"": ""UserPrioritizedRecordShare""
        },
        {
            ""name"": ""UserProvAccount""
        },
        {
            ""name"": ""UserProvAccountStaging""
        },
        {
            ""name"": ""UserProvMockTarget""
        },
        {
            ""name"": ""UserProvisioningConfig""
        },
        {
            ""name"": ""UserProvisioningLog""
        },
        {
            ""name"": ""UserProvisioningRequest""
        },
        {
            ""name"": ""UserProvisioningRequestShare""
        },
        {
            ""name"": ""UserRecordAccess""
        },
        {
            ""name"": ""UserRole""
        },
        {
            ""name"": ""UserSetupEntityAccess""
        },
        {
            ""name"": ""UserShare""
        },
        {
            ""name"": ""VerificationHistory""
        },
        {
            ""name"": ""Visit""
        },
        {
            ""name"": ""VisitFeed""
        },
        {
            ""name"": ""VisitHistory""
        },
        {
            ""name"": ""VisitShare""
        },
        {
            ""name"": ""VisitedParty""
        },
        {
            ""name"": ""VisitedPartyFeed""
        },
        {
            ""name"": ""VisitedPartyHistory""
        },
        {
            ""name"": ""Visitor""
        },
        {
            ""name"": ""VisitorFeed""
        },
        {
            ""name"": ""VisitorHistory""
        },
        {
            ""name"": ""VisualforceAccessMetrics""
        },
        {
            ""name"": ""Vote""
        },
        {
            ""name"": ""WaveAutoInstallRequest""
        },
        {
            ""name"": ""WaveCompatibilityCheckItem""
        },
        {
            ""name"": ""WebLink""
        },
        {
            ""name"": ""WorkOrder""
        },
        {
            ""name"": ""WorkOrderChangeEvent""
        },
        {
            ""name"": ""WorkOrderFeed""
        },
        {
            ""name"": ""WorkOrderHistory""
        },
        {
            ""name"": ""WorkOrderLineItem""
        },
        {
            ""name"": ""WorkOrderLineItemChangeEvent""
        },
        {
            ""name"": ""WorkOrderLineItemFeed""
        },
        {
            ""name"": ""WorkOrderLineItemHistory""
        },
        {
            ""name"": ""WorkOrderLineItemStatus""
        },
        {
            ""name"": ""WorkOrderShare""
        },
        {
            ""name"": ""WorkOrderStatus""
        },
        {
            ""name"": ""WorkPlan""
        },
        {
            ""name"": ""WorkPlanChangeEvent""
        },
        {
            ""name"": ""WorkPlanFeed""
        },
        {
            ""name"": ""WorkPlanHistory""
        },
        {
            ""name"": ""WorkPlanShare""
        },
        {
            ""name"": ""WorkPlanTemplate""
        },
        {
            ""name"": ""WorkPlanTemplateChangeEvent""
        },
        {
            ""name"": ""WorkPlanTemplateEntry""
        },
        {
            ""name"": ""WorkPlanTemplateEntryChangeEvent""
        },
        {
            ""name"": ""WorkPlanTemplateEntryFeed""
        },
        {
            ""name"": ""WorkPlanTemplateEntryHistory""
        },
        {
            ""name"": ""WorkPlanTemplateFeed""
        },
        {
            ""name"": ""WorkPlanTemplateHistory""
        },
        {
            ""name"": ""WorkPlanTemplateShare""
        },
        {
            ""name"": ""WorkStep""
        },
        {
            ""name"": ""WorkStepChangeEvent""
        },
        {
            ""name"": ""WorkStepFeed""
        },
        {
            ""name"": ""WorkStepHistory""
        },
        {
            ""name"": ""WorkStepStatus""
        },
        {
            ""name"": ""WorkStepTemplate""
        },
        {
            ""name"": ""WorkStepTemplateChangeEvent""
        },
        {
            ""name"": ""WorkStepTemplateFeed""
        },
        {
            ""name"": ""WorkStepTemplateHistory""
        },
        {
            ""name"": ""WorkStepTemplateShare""
        }
    ]
}";

            return JObject.Parse(jsonData).ToObject<ApiResponse4>();
        }

        [HttpGet("getcolumns")]
        public async Task<ColumnsResponse> getColumns()
        {
            ColumnsResponse res = new();
            res.Columns = new List<string> { "Customer", "Name", "Address", "City", "ZipCode", "Country", "State", "ShipAddress1", "ShipState", "ShipCity", "ShipZipCode", "ShipCountry", "CompanyID", "CustID"
 };
            res.Status = true;
            return res;
        }

            [HttpGet("sfobjects")]
        public async Task<ApiResponse3> getsfobjects()
        {
            string jsonData = @"{
    ""errMessage"": null,
    ""status"": true,
    ""objectDetails"": {
        ""name"": ""Account"",
        ""fields"": [
            {
                ""id"": ""Id"",
                ""name"": ""Id"",
                ""sftype"": null,
                ""type"": ""id"",
                ""label"": ""Account ID"",
                ""isPrimaryKey"": false,
                ""length"": 18,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""tns:ID"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": []
            },
            {
                ""id"": ""IsDeleted"",
                ""name"": ""IsDeleted"",
                ""sftype"": null,
                ""type"": ""boolean"",
                ""label"": ""Deleted"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:boolean"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": []
            },
            {
                ""id"": ""MasterRecordId"",
                ""name"": ""MasterRecordId"",
                ""sftype"": null,
                ""type"": ""reference"",
                ""label"": ""Master Record ID"",
                ""isPrimaryKey"": false,
                ""length"": 18,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""tns:ID"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": [
                    ""Account""
                ]
            },
            {
                ""id"": ""Name"",
                ""name"": ""Name"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Account Name"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Type"",
                ""name"": ""Type"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Account Type"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""RecordTypeId"",
                ""name"": ""RecordTypeId"",
                ""sftype"": null,
                ""type"": ""reference"",
                ""label"": ""Record Type ID"",
                ""isPrimaryKey"": false,
                ""length"": 18,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""tns:ID"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": [
                    ""RecordType""
                ]
            },
            {
                ""id"": ""ParentId"",
                ""name"": ""ParentId"",
                ""sftype"": null,
                ""type"": ""reference"",
                ""label"": ""Parent Account ID"",
                ""isPrimaryKey"": false,
                ""length"": 18,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""tns:ID"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": [
                    ""Account""
                ]
            },
            {
                ""id"": ""BillingStreet"",
                ""name"": ""BillingStreet"",
                ""sftype"": null,
                ""type"": ""textarea"",
                ""label"": ""Billing Street"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""BillingCity"",
                ""name"": ""BillingCity"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Billing City"",
                ""isPrimaryKey"": false,
                ""length"": 40,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""BillingState"",
                ""name"": ""BillingState"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Billing State/Province"",
                ""isPrimaryKey"": false,
                ""length"": 80,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""BillingPostalCode"",
                ""name"": ""BillingPostalCode"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Billing Zip/Postal Code"",
                ""isPrimaryKey"": false,
                ""length"": 20,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""BillingCountry"",
                ""name"": ""BillingCountry"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Billing Country"",
                ""isPrimaryKey"": false,
                ""length"": 80,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""BillingLatitude"",
                ""name"": ""BillingLatitude"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""Billing Latitude"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""BillingLongitude"",
                ""name"": ""BillingLongitude"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""Billing Longitude"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""BillingGeocodeAccuracy"",
                ""name"": ""BillingGeocodeAccuracy"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Billing Geocode Accuracy"",
                ""isPrimaryKey"": false,
                ""length"": 40,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""BillingAddress"",
                ""name"": ""BillingAddress"",
                ""sftype"": null,
                ""type"": ""address"",
                ""label"": ""Billing Address"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""urn:address"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingStreet"",
                ""name"": ""ShippingStreet"",
                ""sftype"": null,
                ""type"": ""textarea"",
                ""label"": ""Shipping Street"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingCity"",
                ""name"": ""ShippingCity"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Shipping City"",
                ""isPrimaryKey"": false,
                ""length"": 40,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingState"",
                ""name"": ""ShippingState"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Shipping State/Province"",
                ""isPrimaryKey"": false,
                ""length"": 80,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingPostalCode"",
                ""name"": ""ShippingPostalCode"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Shipping Zip/Postal Code"",
                ""isPrimaryKey"": false,
                ""length"": 20,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingCountry"",
                ""name"": ""ShippingCountry"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Shipping Country"",
                ""isPrimaryKey"": false,
                ""length"": 80,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingLatitude"",
                ""name"": ""ShippingLatitude"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""Shipping Latitude"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingLongitude"",
                ""name"": ""ShippingLongitude"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""Shipping Longitude"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingGeocodeAccuracy"",
                ""name"": ""ShippingGeocodeAccuracy"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Shipping Geocode Accuracy"",
                ""isPrimaryKey"": false,
                ""length"": 40,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ShippingAddress"",
                ""name"": ""ShippingAddress"",
                ""sftype"": null,
                ""type"": ""address"",
                ""label"": ""Shipping Address"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""urn:address"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Phone"",
                ""name"": ""Phone"",
                ""sftype"": null,
                ""type"": ""phone"",
                ""label"": ""Account Phone"",
                ""isPrimaryKey"": false,
                ""length"": 40,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Fax"",
                ""name"": ""Fax"",
                ""sftype"": null,
                ""type"": ""phone"",
                ""label"": ""Account Fax"",
                ""isPrimaryKey"": false,
                ""length"": 40,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""AccountNumber"",
                ""name"": ""AccountNumber"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Account Number"",
                ""isPrimaryKey"": false,
                ""length"": 40,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Website"",
                ""name"": ""Website"",
                ""sftype"": null,
                ""type"": ""url"",
                ""label"": ""Website"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""PhotoUrl"",
                ""name"": ""PhotoUrl"",
                ""sftype"": null,
                ""type"": ""url"",
                ""label"": ""Photo URL"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Sic"",
                ""name"": ""Sic"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""SIC Code"",
                ""isPrimaryKey"": false,
                ""length"": 20,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Industry"",
                ""name"": ""Industry"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Industry"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""AnnualRevenue"",
                ""name"": ""AnnualRevenue"",
                ""sftype"": null,
                ""type"": ""currency"",
                ""label"": ""Annual Revenue"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""NumberOfEmployees"",
                ""name"": ""NumberOfEmployees"",
                ""sftype"": null,
                ""type"": ""int"",
                ""label"": ""Employees"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:int"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Ownership"",
                ""name"": ""Ownership"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Ownership"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""TickerSymbol"",
                ""name"": ""TickerSymbol"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Ticker Symbol"",
                ""isPrimaryKey"": false,
                ""length"": 20,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Description"",
                ""name"": ""Description"",
                ""sftype"": null,
                ""type"": ""textarea"",
                ""label"": ""Account Description"",
                ""isPrimaryKey"": false,
                ""length"": 32000,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Rating"",
                ""name"": ""Rating"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Account Rating"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Site"",
                ""name"": ""Site"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Account Site"",
                ""isPrimaryKey"": false,
                ""length"": 80,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""OwnerId"",
                ""name"": ""OwnerId"",
                ""sftype"": null,
                ""type"": ""reference"",
                ""label"": ""Owner ID"",
                ""isPrimaryKey"": false,
                ""length"": 18,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""tns:ID"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": [
                    ""User""
                ]
            },
            {
                ""id"": ""CreatedDate"",
                ""name"": ""CreatedDate"",
                ""sftype"": null,
                ""type"": ""datetime"",
                ""label"": ""Created Date"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:dateTime"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": []
            },
            {
                ""id"": ""CreatedById"",
                ""name"": ""CreatedById"",
                ""sftype"": null,
                ""type"": ""reference"",
                ""label"": ""Created By ID"",
                ""isPrimaryKey"": false,
                ""length"": 18,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""tns:ID"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": [
                    ""User""
                ]
            },
            {
                ""id"": ""LastModifiedDate"",
                ""name"": ""LastModifiedDate"",
                ""sftype"": null,
                ""type"": ""datetime"",
                ""label"": ""Last Modified Date"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:dateTime"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": []
            },
            {
                ""id"": ""LastModifiedById"",
                ""name"": ""LastModifiedById"",
                ""sftype"": null,
                ""type"": ""reference"",
                ""label"": ""Last Modified By ID"",
                ""isPrimaryKey"": false,
                ""length"": 18,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""tns:ID"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": [
                    ""User""
                ]
            },
            {
                ""id"": ""SystemModstamp"",
                ""name"": ""SystemModstamp"",
                ""sftype"": null,
                ""type"": ""datetime"",
                ""label"": ""System Modstamp"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:dateTime"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": []
            },
            {
                ""id"": ""LastActivityDate"",
                ""name"": ""LastActivityDate"",
                ""sftype"": null,
                ""type"": ""date"",
                ""label"": ""Last Activity"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:date"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""LastViewedDate"",
                ""name"": ""LastViewedDate"",
                ""sftype"": null,
                ""type"": ""datetime"",
                ""label"": ""Last Viewed Date"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:dateTime"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""LastReferencedDate"",
                ""name"": ""LastReferencedDate"",
                ""sftype"": null,
                ""type"": ""datetime"",
                ""label"": ""Last Referenced Date"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:dateTime"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""IsPartner"",
                ""name"": ""IsPartner"",
                ""sftype"": null,
                ""type"": ""boolean"",
                ""label"": ""Partner Account"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:boolean"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": []
            },
            {
                ""id"": ""IsCustomerPortal"",
                ""name"": ""IsCustomerPortal"",
                ""sftype"": null,
                ""type"": ""boolean"",
                ""label"": ""Customer Portal Account"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": false,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:boolean"",
                ""defaultedOnCreate"": true,
                ""referenceTo"": []
            },
            {
                ""id"": ""ChannelProgramName"",
                ""name"": ""ChannelProgramName"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Channel Program Name"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""ChannelProgramLevelName"",
                ""name"": ""ChannelProgramLevelName"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Channel Program Level Name"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Jigsaw"",
                ""name"": ""Jigsaw"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Data.com Key"",
                ""isPrimaryKey"": false,
                ""length"": 20,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""JigsawCompanyId"",
                ""name"": ""JigsawCompanyId"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Jigsaw Company ID"",
                ""isPrimaryKey"": false,
                ""length"": 20,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": false,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""AccountSource"",
                ""name"": ""AccountSource"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Account Source"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""DunsNumber"",
                ""name"": ""DunsNumber"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""D-U-N-S Number"",
                ""isPrimaryKey"": false,
                ""length"": 9,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Tradestyle"",
                ""name"": ""Tradestyle"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Tradestyle"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""NaicsCode"",
                ""name"": ""NaicsCode"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""NAICS Code"",
                ""isPrimaryKey"": false,
                ""length"": 8,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""NaicsDesc"",
                ""name"": ""NaicsDesc"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""NAICS Description"",
                ""isPrimaryKey"": false,
                ""length"": 120,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""YearStarted"",
                ""name"": ""YearStarted"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Year Started"",
                ""isPrimaryKey"": false,
                ""length"": 4,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""SicDesc"",
                ""name"": ""SicDesc"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""SIC Description"",
                ""isPrimaryKey"": false,
                ""length"": 80,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""DandbCompanyId"",
                ""name"": ""DandbCompanyId"",
                ""sftype"": null,
                ""type"": ""reference"",
                ""label"": ""D&B Company ID"",
                ""isPrimaryKey"": false,
                ""length"": 18,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": false,
                ""createable"": true,
                ""soapType"": ""tns:ID"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": [
                    ""DandBCompany""
                ]
            },
            {
                ""id"": ""CustomerPriority__c"",
                ""name"": ""CustomerPriority__c"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Customer Priority"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""SLA__c"",
                ""name"": ""SLA__c"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""SLA"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Active__c"",
                ""name"": ""Active__c"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Active"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""NumberofLocations__c"",
                ""name"": ""NumberofLocations__c"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""Number of Locations"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""UpsellOpportunity__c"",
                ""name"": ""UpsellOpportunity__c"",
                ""sftype"": null,
                ""type"": ""picklist"",
                ""label"": ""Upsell Opportunity"",
                ""isPrimaryKey"": false,
                ""length"": 255,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""SLASerialNumber__c"",
                ""name"": ""SLASerialNumber__c"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""SLA Serial Number"",
                ""isPrimaryKey"": false,
                ""length"": 10,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""SLAExpirationDate__c"",
                ""name"": ""SLAExpirationDate__c"",
                ""sftype"": null,
                ""type"": ""date"",
                ""label"": ""SLA Expiration Date"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:date"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Company__c"",
                ""name"": ""Company__c"",
                ""sftype"": null,
                ""type"": ""string"",
                ""label"": ""Company"",
                ""isPrimaryKey"": false,
                ""length"": 12,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:string"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""CustKey__c"",
                ""name"": ""CustKey__c"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""CustKey"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": true,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": true,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Account_Distance__c"",
                ""name"": ""Account_Distance__c"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""Delivery Distance"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Billing_Address_Latitude__c"",
                ""name"": ""Billing_Address_Latitude__c"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""Billing Address Latitude"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Win_Rate_against_Competitor__c"",
                ""name"": ""Win_Rate_against_Competitor__c"",
                ""sftype"": null,
                ""type"": ""percent"",
                ""label"": ""Win Rate against Competitor"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Total_Opportunity_Amount_Contested__c"",
                ""name"": ""Total_Opportunity_Amount_Contested__c"",
                ""sftype"": null,
                ""type"": ""currency"",
                ""label"": ""Total Opportunity Amount Contested"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Total_Pipeline_Amount_Contested__c"",
                ""name"": ""Total_Pipeline_Amount_Contested__c"",
                ""sftype"": null,
                ""type"": ""currency"",
                ""label"": ""Total Pipeline Amount Contested"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Total_contested_opportunity_amount_lost__c"",
                ""name"": ""Total_contested_opportunity_amount_lost__c"",
                ""sftype"": null,
                ""type"": ""currency"",
                ""label"": ""Total Contested Opportunity Amount Lost"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""Total_contested_opportunity_amount_won__c"",
                ""name"": ""Total_contested_opportunity_amount_won__c"",
                ""sftype"": null,
                ""type"": ""currency"",
                ""label"": ""Total Contested Opportunity Amount Won"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""num_of_Opportunities_Contested__c"",
                ""name"": ""num_of_Opportunities_Contested__c"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""# of Opportunities Contested"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""num_of_Pipeline_Opportunities_contested__c"",
                ""name"": ""num_of_Pipeline_Opportunities_contested__c"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""# of Pipeline Opportunities Contested"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""num_of_contested_opportunities_Won__c"",
                ""name"": ""num_of_contested_opportunities_Won__c"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""# of Contested Opportunities Won"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            },
            {
                ""id"": ""num_of_contested_opportunities_lost__c"",
                ""name"": ""num_of_contested_opportunities_lost__c"",
                ""sftype"": null,
                ""type"": ""double"",
                ""label"": ""# of Contested Opportunities Lost"",
                ""isPrimaryKey"": false,
                ""length"": 0,
                ""isNullable"": false,
                ""externalId"": false,
                ""nillable"": true,
                ""custom"": true,
                ""createable"": false,
                ""soapType"": ""xsd:double"",
                ""defaultedOnCreate"": false,
                ""referenceTo"": []
            }
        ]
    }
}";

            ApiResponse3 res = new();
            res.data = new();
            var data = JObject.Parse(jsonData).ToObject<ApiResponse2>();
            foreach (var item in data.objectDetails.fields)
            {

                if (item.nillable == false && item.defaultedOnCreate == false && item.createable == true)
                {
                    res.data.Add(new() { Id = item.name, Name = item.label + " (" + item.name + ")", Category = "Required" , IsExternalId = item.externalId, IsLookup = item.referenceTo != null && item.referenceTo.Count > 0, LookupName = item.referenceTo?.FirstOrDefault()
                    });
                }
                else if (item.custom == true)
                {
                    res.data.Add(new() { Id = item.name, Name = item.label + " (" + item.name + ")", Category = "Custom", IsExternalId = item.externalId,
                        IsLookup = item.referenceTo != null && item.referenceTo.Count > 0,
                        LookupName = item.referenceTo?.FirstOrDefault()
                    });
                }
                else if (item.soapType == "tns:ID")
                {
                    res.data.Add(new() { Id = item.name, Name = item.label + " (" + item.name + ")", Category = "Ids", IsExternalId = item.externalId,
                        IsLookup = item.referenceTo != null && item.referenceTo.Count > 0,
                        LookupName = item.referenceTo?.FirstOrDefault()
                    });
                }
                else
                {
                    res.data.Add(new() { Id = item.name, Name = item.label + " (" + item.name + ")", Category = "Unmapped", IsExternalId = item.externalId,
                        IsLookup = item.referenceTo != null && item.referenceTo.Count > 0,
                        LookupName = item.referenceTo?.FirstOrDefault()
                    });
                }

            }

            return res;
        }
    }
}
