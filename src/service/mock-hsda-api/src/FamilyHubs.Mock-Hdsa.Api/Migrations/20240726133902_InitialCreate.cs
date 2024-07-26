﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyHubs.Mock_Hdsa.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MockResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScenarioName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathParams = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryParams = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockResponses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MockResponses",
                columns: new[] { "Id", "OperationName", "PathParams", "QueryParams", "ResponseBody", "ScenarioName", "StatusCode" },
                values: new object[,]
                {
                    { 1, "getAPIMetaInformation", null, null, "{\r\n  \"version\": \"3.0\",\r\n  \"profile\": \"https://todo/put/our/profile/uri/here\",\r\n  \"openapi_url\": \"https://raw.githubusercontent.com/openreferral/specification/3.0/schema/openapi.json\"\r\n}\r\n", null, 200 },
                    { 2, "getFullyNestedServiceById", "id=ac148810-d857-441c-9679-408f346de14b", null, "{\r\n  \"id\": \"ac148810-d857-441c-9679-408f346de14b\",\r\n  \"name\": \"Community Counselling\",\r\n  \"alternate_name\": \"MyCity Counselling Services\",\r\n  \"description\": \"Counselling Services provided by trained professionals. Suitable for people with mental health conditions such as anxiety, depression, or eating disorders as well as people experiencing difficult life events and circumstances. \",\r\n  \"url\": \"http://example.com/counselling\",\r\n  \"email\": \"email@example.com\",\r\n  \"status\": \"active\",\r\n  \"interpretation_services\": \"Interpretation services are available in Urdu, Polish, and Slovak\",\r\n  \"application_process\": \"If you are an NHS patient please ask your GP for a referral letter, we will then be in touch with you directly. If you are not an NHS patient you should ring our reception to arrange an appointment\",\r\n  \"fees_description\": \"Non-NHS patients are expected to pay for their counselling sessions. We charge a flat rate per hour of counselling. The current rate is \\u00a350 per hour. Please see our website for up to date prices.\",\r\n  \"wait_time\": \"wait_time\",\r\n  \"fees\": \"fees_description\",\r\n  \"accreditations\": \"All of our practitioners are accredited by the BASC, UKCP, and the Professional Standards Body\",\r\n  \"eligibility_description\": \"This service is intended for all people aged 12 and over who require counselling services in the MyCity area\",\r\n  \"minimum_age\": 12,\r\n  \"maximum_age\": 100,\r\n  \"assured_date\": \"2005-01-01\",\r\n  \"assurer_email\": \"email@example.com\",\r\n  \"licenses\": \"licences\",\r\n  \"alert\": \"Following COVID-19 we have moved most of our counselling sessions online. Please contact the reception if you require further information.\",\r\n  \"last_modified\": \"2023-03-15T10:30:45.123Z\",\r\n  \"phones\": [\r\n    {\r\n      \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n      \"number\": \"\\\"+44 1234 234567\\\"\",\r\n      \"extension\": 100,\r\n      \"type\": \"voice\",\r\n      \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n      \"languages\": [\r\n        {\r\n          \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n          \"name\": \"Urdu\",\r\n          \"code\": \"ur\",\r\n          \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n        }\r\n      ]\r\n    }\r\n  ],\r\n  \"schedules\": [\r\n    {\r\n      \"id\": \"48102e86-bb50-41c4-8f1e-e269368c41d1\",\r\n      \"valid_from\": \"2005-01-01\",\r\n      \"valid_to\": \"2005-01-01\",\r\n      \"dtstart\": \"2005-01-01\",\r\n      \"timezone\": 3,\r\n      \"until\": \"2005-01-01\",\r\n      \"count\": 3,\r\n      \"wkst\": \"TU\",\r\n      \"freq\": \"WEEKLY\",\r\n      \"interval\": 1,\r\n      \"byday\": \"TH,FR\",\r\n      \"byweekno\": \"41,42\",\r\n      \"bymonthday\": \"2,3,4\",\r\n      \"byyearday\": \"2,3,4\",\r\n      \"description\": \"The service is available from 10 am to 6pm weekdays. The service is not available on public holidays.\",\r\n      \"opens_at\": \"10:00:00\",\r\n      \"closes_at\": \"18:00:00\",\r\n      \"schedule_link\": \"http://example.com\",\r\n      \"attending_type\": \"You can attend this service in-person or remotely via video conferencing\",\r\n      \"notes\": \"Some of our staff will be unavailable during their lunch break which may be any 30 minute period between 1200 and 1400\"\r\n    }\r\n  ],\r\n  \"service_areas\": [\r\n    {\r\n      \"id\": \"381c64f1-a724-4884-9c21-ac96c21cca3e\",\r\n      \"name\": \"The service is available to all residents of the MyCity area. We provide the service in our offices or via video conferencing to any who can provide evidence of their residency.\",\r\n      \"description\": \"The service is available to all residents of the MyCity area, including all postcodes starting with AB1, AB2, and AB3\",\r\n      \"extent\": \"extent\",\r\n      \"extent_type\": \"geojson\",\r\n      \"uri\": \"http://example.com\"\r\n    }\r\n  ],\r\n  \"service_at_locations\": [\r\n    {\r\n      \"id\": \"e94c9f38-1e8f-4564-91d4-d53501ab1765\",\r\n      \"description\": \"Counselling Services provided by trained professionals in the MyCity area.\",\r\n      \"contacts\": [\r\n        {\r\n          \"id\": \"1e7efce3-639b-4880-940c-b95cd30cdb50\",\r\n          \"name\": \"Ann Persona\",\r\n          \"title\": \"Reception Manager\",\r\n          \"department\": \"Reception\",\r\n          \"email\": \"email@example.com\",\r\n          \"phones\": [\r\n            {\r\n              \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n              \"number\": \"\\\"+44 1234 234567\\\"\",\r\n              \"extension\": 100,\r\n              \"type\": \"voice\",\r\n              \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n              \"languages\": [\r\n                {\r\n                  \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n                  \"name\": \"Urdu\",\r\n                  \"code\": \"ur\",\r\n                  \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n                }\r\n              ]\r\n            }\r\n          ]\r\n        }\r\n      ],\r\n      \"phones\": [\r\n        {\r\n          \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n          \"number\": \"\\\"+44 1234 234567\\\"\",\r\n          \"extension\": 100,\r\n          \"type\": \"voice\",\r\n          \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n          \"languages\": [\r\n            {\r\n              \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n              \"name\": \"Urdu\",\r\n              \"code\": \"ur\",\r\n              \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n            }\r\n          ]\r\n        }\r\n      ],\r\n      \"schedules\": [\r\n        {\r\n          \"id\": \"48102e86-bb50-41c4-8f1e-e269368c41d1\",\r\n          \"valid_from\": \"2005-01-01\",\r\n          \"valid_to\": \"2005-01-01\",\r\n          \"dtstart\": \"2005-01-01\",\r\n          \"timezone\": 3,\r\n          \"until\": \"2005-01-01\",\r\n          \"count\": 3,\r\n          \"wkst\": \"TU\",\r\n          \"freq\": \"WEEKLY\",\r\n          \"interval\": 1,\r\n          \"byday\": \"TH,FR\",\r\n          \"byweekno\": \"41,42\",\r\n          \"bymonthday\": \"2,3,4\",\r\n          \"byyearday\": \"2,3,4\",\r\n          \"description\": \"The service is available from 10 am to 6pm weekdays. The service is not available on public holidays.\",\r\n          \"opens_at\": \"10:00:00\",\r\n          \"closes_at\": \"18:00:00\",\r\n          \"schedule_link\": \"http://example.com\",\r\n          \"attending_type\": \"You can attend this service in-person or remotely via video conferencing\",\r\n          \"notes\": \"Some of our staff will be unavailable during their lunch break which may be any 30 minute period between 1200 and 1400\"\r\n        }\r\n      ],\r\n      \"location\": {\r\n        \"id\": \"3a19ff88-4620-4d17-9830-ac1d859eb5d5\",\r\n        \"location_type\": \"physical\",\r\n        \"url\": \"http://example.com\",\r\n        \"name\": \"MyCity Civic Center\",\r\n        \"alternate_name\": \"Civic Center\",\r\n        \"description\": \"MyCity Civic Center is located on Main Street and contains facilities for a number of civic and community services available.\",\r\n        \"transportation\": \"MyCity Civic Center is serviced by the number 1 Bus and is a short walk from the Main Street Metro Station\",\r\n        \"latitude\": 100,\r\n        \"longitude\": 101,\r\n        \"external_identifier\": \"10092008082\",\r\n        \"external_identifier_type\": \"UPRN\",\r\n        \"languages\": [\r\n          {\r\n            \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n            \"name\": \"Urdu\",\r\n            \"code\": \"ur\",\r\n            \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n          }\r\n        ],\r\n        \"addresses\": [\r\n          {\r\n            \"id\": \"74706e55-df26-4b84-80fe-ecc30b5befb4\",\r\n            \"attention\": \"A. Persona\",\r\n            \"address_1\": \"1-30 Main Street\",\r\n            \"address_2\": \"MyVillage\",\r\n            \"city\": \"MyCity\",\r\n            \"region\": \"MyRegion\",\r\n            \"state_province\": \"MyState\",\r\n            \"postal_code\": \"ABC 1234\",\r\n            \"country\": \"US\",\r\n            \"address_type\": \"postal\"\r\n          }\r\n        ],\r\n        \"contacts\": [\r\n          {\r\n            \"id\": \"1e7efce3-639b-4880-940c-b95cd30cdb50\",\r\n            \"name\": \"Ann Persona\",\r\n            \"title\": \"Reception Manager\",\r\n            \"department\": \"Reception\",\r\n            \"email\": \"email@example.com\",\r\n            \"phones\": [\r\n              {\r\n                \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n                \"number\": \"\\\"+44 1234 234567\\\"\",\r\n                \"extension\": 100,\r\n                \"type\": \"voice\",\r\n                \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n                \"languages\": [\r\n                  {\r\n                    \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n                    \"name\": \"Urdu\",\r\n                    \"code\": \"ur\",\r\n                    \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n                  }\r\n                ]\r\n              }\r\n            ]\r\n          }\r\n        ],\r\n        \"accessibility\": [\r\n          {\r\n            \"id\": \"afcf296e-1cb2-4139-9c88-33d587d1a50b\",\r\n            \"description\": \"The building is equipped with induction loops for hearing aids. Wheelchair access is possible on all levels.\",\r\n            \"details\": \"Switch hearing aid to T-coil to access the induction loop facility.\",\r\n            \"url\": \"http://example.com\"\r\n          }\r\n        ],\r\n        \"phones\": [\r\n          {\r\n            \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n            \"number\": \"\\\"+44 1234 234567\\\"\",\r\n            \"extension\": 100,\r\n            \"type\": \"voice\",\r\n            \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n            \"languages\": [\r\n              {\r\n                \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n                \"name\": \"Urdu\",\r\n                \"code\": \"ur\",\r\n                \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n              }\r\n            ]\r\n          }\r\n        ],\r\n        \"schedules\": [\r\n          {\r\n            \"id\": \"48102e86-bb50-41c4-8f1e-e269368c41d1\",\r\n            \"valid_from\": \"2005-01-01\",\r\n            \"valid_to\": \"2005-01-01\",\r\n            \"dtstart\": \"2005-01-01\",\r\n            \"timezone\": 3,\r\n            \"until\": \"2005-01-01\",\r\n            \"count\": 3,\r\n            \"wkst\": \"TU\",\r\n            \"freq\": \"WEEKLY\",\r\n            \"interval\": 1,\r\n            \"byday\": \"TH,FR\",\r\n            \"byweekno\": \"41,42\",\r\n            \"bymonthday\": \"2,3,4\",\r\n            \"byyearday\": \"2,3,4\",\r\n            \"description\": \"The service is available from 10 am to 6pm weekdays. The service is not available on public holidays.\",\r\n            \"opens_at\": \"10:00:00\",\r\n            \"closes_at\": \"18:00:00\",\r\n            \"schedule_link\": \"http://example.com\",\r\n            \"attending_type\": \"You can attend this service in-person or remotely via video conferencing\",\r\n            \"notes\": \"Some of our staff will be unavailable during their lunch break which may be any 30 minute period between 1200 and 1400\"\r\n          }\r\n        ]\r\n      }\r\n    }\r\n  ],\r\n  \"languages\": [\r\n    {\r\n      \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n      \"name\": \"Urdu\",\r\n      \"code\": \"ur\",\r\n      \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n    }\r\n  ],\r\n  \"organization\": {\r\n    \"id\": \"d9d5e0f5-d3ce-4f73-9a2f-4dd0ecc6c610\",\r\n    \"name\": \"Example Organization Inc.\",\r\n    \"alternate_name\": \"Example Org\",\r\n    \"description\": \"Example Org is a non-profit organization dedicated to providing services to qualified beneficiaries\",\r\n    \"email\": \"email@example.com\",\r\n    \"website\": \"http://example.com\",\r\n    \"tax_status\": \"tax_status\",\r\n    \"year_incorporated\": 2011,\r\n    \"legal_status\": \"Limited Company\",\r\n    \"logo\": \"https://openreferral.org/wp-content/uploads/2018/02/OpenReferral_Logo_Green-4-1.png\",\r\n    \"uri\": \"http://example.com\",\r\n    \"parent_organization_id\": \"cd09a387-91f4-4555-94ec-e799c35344cd\",\r\n    \"funding\": [\r\n      {\r\n        \"id\": \"1f2df32c-bf08-4b8e-bd6f-e834014b19bc\",\r\n        \"source\": \"This service is funded partially by the MyCity local government and via grants made by charity funding bodies. We also operate a for-profit department which is used to partially cover the expense of the public service\"\r\n      }\r\n    ],\r\n    \"contacts\": [\r\n      {\r\n        \"id\": \"1e7efce3-639b-4880-940c-b95cd30cdb50\",\r\n        \"name\": \"Ann Persona\",\r\n        \"title\": \"Reception Manager\",\r\n        \"department\": \"Reception\",\r\n        \"email\": \"email@example.com\",\r\n        \"phones\": [\r\n          {\r\n            \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n            \"number\": \"\\\"+44 1234 234567\\\"\",\r\n            \"extension\": 100,\r\n            \"type\": \"voice\",\r\n            \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n            \"languages\": [\r\n              {\r\n                \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n                \"name\": \"Urdu\",\r\n                \"code\": \"ur\",\r\n                \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n              }\r\n            ]\r\n          }\r\n        ]\r\n      }\r\n    ],\r\n    \"phones\": [\r\n      {\r\n        \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n        \"number\": \"\\\"+44 1234 234567\\\"\",\r\n        \"extension\": 100,\r\n        \"type\": \"voice\",\r\n        \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n        \"languages\": [\r\n          {\r\n            \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n            \"name\": \"Urdu\",\r\n            \"code\": \"ur\",\r\n            \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n          }\r\n        ]\r\n      }\r\n    ],\r\n    \"locations\": [\r\n      {\r\n        \"id\": \"3a19ff88-4620-4d17-9830-ac1d859eb5d5\",\r\n        \"location_type\": \"physical\",\r\n        \"url\": \"http://example.com\",\r\n        \"name\": \"MyCity Civic Center\",\r\n        \"alternate_name\": \"Civic Center\",\r\n        \"description\": \"MyCity Civic Center is located on Main Street and contains facilities for a number of civic and community services available.\",\r\n        \"transportation\": \"MyCity Civic Center is serviced by the number 1 Bus and is a short walk from the Main Street Metro Station\",\r\n        \"latitude\": 100,\r\n        \"longitude\": 101,\r\n        \"external_identifier\": \"10092008082\",\r\n        \"external_identifier_type\": \"UPRN\",\r\n        \"languages\": [\r\n          {\r\n            \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n            \"name\": \"Urdu\",\r\n            \"code\": \"ur\",\r\n            \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n          }\r\n        ],\r\n        \"addresses\": [\r\n          {\r\n            \"id\": \"74706e55-df26-4b84-80fe-ecc30b5befb4\",\r\n            \"attention\": \"A. Persona\",\r\n            \"address_1\": \"1-30 Main Street\",\r\n            \"address_2\": \"MyVillage\",\r\n            \"city\": \"MyCity\",\r\n            \"region\": \"MyRegion\",\r\n            \"state_province\": \"MyState\",\r\n            \"postal_code\": \"ABC 1234\",\r\n            \"country\": \"US\",\r\n            \"address_type\": \"postal\"\r\n          }\r\n        ],\r\n        \"contacts\": [\r\n          {\r\n            \"id\": \"1e7efce3-639b-4880-940c-b95cd30cdb50\",\r\n            \"name\": \"Ann Persona\",\r\n            \"title\": \"Reception Manager\",\r\n            \"department\": \"Reception\",\r\n            \"email\": \"email@example.com\",\r\n            \"phones\": [\r\n              {\r\n                \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n                \"number\": \"\\\"+44 1234 234567\\\"\",\r\n                \"extension\": 100,\r\n                \"type\": \"voice\",\r\n                \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n                \"languages\": [\r\n                  {\r\n                    \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n                    \"name\": \"Urdu\",\r\n                    \"code\": \"ur\",\r\n                    \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n                  }\r\n                ]\r\n              }\r\n            ]\r\n          }\r\n        ],\r\n        \"accessibility\": [\r\n          {\r\n            \"id\": \"afcf296e-1cb2-4139-9c88-33d587d1a50b\",\r\n            \"description\": \"The building is equipped with induction loops for hearing aids. Wheelchair access is possible on all levels.\",\r\n            \"details\": \"Switch hearing aid to T-coil to access the induction loop facility.\",\r\n            \"url\": \"http://example.com\"\r\n          }\r\n        ],\r\n        \"phones\": [\r\n          {\r\n            \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n            \"number\": \"\\\"+44 1234 234567\\\"\",\r\n            \"extension\": 100,\r\n            \"type\": \"voice\",\r\n            \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n            \"languages\": [\r\n              {\r\n                \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n                \"name\": \"Urdu\",\r\n                \"code\": \"ur\",\r\n                \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n              }\r\n            ]\r\n          }\r\n        ],\r\n        \"schedules\": [\r\n          {\r\n            \"id\": \"48102e86-bb50-41c4-8f1e-e269368c41d1\",\r\n            \"valid_from\": \"2005-01-01\",\r\n            \"valid_to\": \"2005-01-01\",\r\n            \"dtstart\": \"2005-01-01\",\r\n            \"timezone\": 3,\r\n            \"until\": \"2005-01-01\",\r\n            \"count\": 3,\r\n            \"wkst\": \"TU\",\r\n            \"freq\": \"WEEKLY\",\r\n            \"interval\": 1,\r\n            \"byday\": \"TH,FR\",\r\n            \"byweekno\": \"41,42\",\r\n            \"bymonthday\": \"2,3,4\",\r\n            \"byyearday\": \"2,3,4\",\r\n            \"description\": \"The service is available from 10 am to 6pm weekdays. The service is not available on public holidays.\",\r\n            \"opens_at\": \"10:00:00\",\r\n            \"closes_at\": \"18:00:00\",\r\n            \"schedule_link\": \"http://example.com\",\r\n            \"attending_type\": \"You can attend this service in-person or remotely via video conferencing\",\r\n            \"notes\": \"Some of our staff will be unavailable during their lunch break which may be any 30 minute period between 1200 and 1400\"\r\n          }\r\n        ]\r\n      }\r\n    ],\r\n    \"programs\": [\r\n      {\r\n        \"id\": \"e7ec2e57-4540-43fa-b2c7-6be5a0ef7f42\",\r\n        \"name\": \"Community Mental Health Support\",\r\n        \"alternate_name\": \"MyCity Mental Health Group\",\r\n        \"description\": \"Comprehensive Mental Health Services available to residents of MyCity including CBT and Counselling. This is not an emergency service and should not be used as an alternative to hospital and GP services.\"\r\n      }\r\n    ],\r\n    \"organization_identifiers\": [\r\n      {\r\n        \"id\": \"d4dbcebc-0802-47cb-8651-b937ac4f2f3e\",\r\n        \"identifier_scheme\": \"GB-COH\",\r\n        \"identifier_type\": \"Company number\",\r\n        \"identifier\": \"1234567\"\r\n      }\r\n    ]\r\n  },\r\n  \"funding\": [\r\n    {\r\n      \"id\": \"1f2df32c-bf08-4b8e-bd6f-e834014b19bc\",\r\n      \"source\": \"This service is funded partially by the MyCity local government and via grants made by charity funding bodies. We also operate a for-profit department which is used to partially cover the expense of the public service\"\r\n    }\r\n  ],\r\n  \"cost_options\": [\r\n    {\r\n      \"id\": \"1fdf4d39-3d80-484d-9f92-a8ffa08621e7\",\r\n      \"valid_from\": \"2020-01-01\",\r\n      \"valid_to\": \"2022-12-31\",\r\n      \"option\": \"Under 18s and Over 60s\",\r\n      \"currency\": \"gbp\",\r\n      \"amount\": 35,\r\n      \"amount_description\": \"per 1 hour session\"\r\n    }\r\n  ],\r\n  \"program\": {\r\n    \"id\": \"e7ec2e57-4540-43fa-b2c7-6be5a0ef7f42\",\r\n    \"name\": \"Community Mental Health Support\",\r\n    \"alternate_name\": \"MyCity Mental Health Group\",\r\n    \"description\": \"Comprehensive Mental Health Services available to residents of MyCity including CBT and Counselling. This is not an emergency service and should not be used as an alternative to hospital and GP services.\"\r\n  },\r\n  \"required_documents\": [\r\n    {\r\n      \"id\": \"f6ad7e69-b9c8-42ce-92db-92cedb4c05c0\",\r\n      \"document\": \"Any official identification document (Passport, Driver's Licence, identity card) and evidence of address such as an official letter from local or national government. A driver's license which shows your name, photograph, and address can be used as a sole identification document.\",\r\n      \"uri\": \"http://example.com\"\r\n    }\r\n  ],\r\n  \"contacts\": [\r\n    {\r\n      \"id\": \"1e7efce3-639b-4880-940c-b95cd30cdb50\",\r\n      \"name\": \"Ann Persona\",\r\n      \"title\": \"Reception Manager\",\r\n      \"department\": \"Reception\",\r\n      \"email\": \"email@example.com\",\r\n      \"phones\": [\r\n        {\r\n          \"id\": \"1554f2e2-a373-45db-a3fa-9fc48a61c15e\",\r\n          \"number\": \"\\\"+44 1234 234567\\\"\",\r\n          \"extension\": 100,\r\n          \"type\": \"voice\",\r\n          \"description\": \"Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.\",\r\n          \"languages\": [\r\n            {\r\n              \"id\": \"2989d3ed-c547-48f8-8f9d-432d81c7892e\",\r\n              \"name\": \"Urdu\",\r\n              \"code\": \"ur\",\r\n              \"note\": \"Translation services provided via bilingual staff when they are available\"\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }\r\n  ],\r\n  \"attributes\": [\r\n    {\r\n      \"id\": \"ae58cc39-8b70-4ab1-8aea-786882e5ac8e\",\r\n      \"link_type\": \"link_type\",\r\n      \"link_entity\": \"link_entity\",\r\n      \"value\": \"value\",\r\n      \"taxonomy_term\": {\r\n        \"id\": \"3f7b145d-84af-42d7-8fae-eaca714b02b2\",\r\n        \"code\": \"code\",\r\n        \"name\": \"name\",\r\n        \"description\": \"description\",\r\n        \"parent_id\": \"0bc248fa-dc27-4650-9ba4-8f1a24ef16a2\",\r\n        \"taxonomy\": \"taxonomy\",\r\n        \"taxonomy_detail\": {\r\n          \"id\": \"5c4d79d7-cc55-470e-9f1f-8cad074e4892\",\r\n          \"name\": \"name\",\r\n          \"description\": \"description\",\r\n          \"uri\": \"http://example.com\",\r\n          \"version\": \"version\"\r\n        },\r\n        \"language\": \"eng\",\r\n        \"term_uri\": \"http://example.com\"\r\n      }\r\n    }\r\n  ],\r\n  \"metadata\": [\r\n    {\r\n      \"id\": \"6cd71e9f-1013-49d9-8370-26b8f59d3e5a\",\r\n      \"resource_type\": \"location\",\r\n      \"last_action_date\": \"2011-01-01\",\r\n      \"last_action_type\": \"update\",\r\n      \"field_name\": \"name\",\r\n      \"previous_value\": \"MyCity Civic Center\",\r\n      \"replacement_value\": \"MyCity New Civic Center\",\r\n      \"updated_by\": \"Ann Persona\"\r\n    }\r\n  ]\r\n}", null, 200 },
                    { 3, "getPaginatedListOfServices", null, null, "    {\r\n      \"total_items\": 1,\r\n      \"total_pages\": 1,\r\n      \"page_number\": 1,\r\n      \"size\": 1,\r\n      \"first_page\": true,\r\n      \"last_page\": false,\r\n      \"empty\": false,\r\n      \"contents\": [\r\n        {\r\n          \"id\": \"ac148810-d857-441c-9679-408f346de14b\",\r\n          \"name\": \"Community Counselling\",\r\n          \"alternate_name\": \"MyCity Counselling Services\",\r\n          \"description\": \"Counselling Services provided by trained professionals. Suitable for people with mental health conditions such as anxiety, depression, or eating disorders as well as people experiencing difficult life events and circumstances. \",\r\n          \"url\": \"http://example.com/counselling\",\r\n          \"email\": \"email@example.com\",\r\n          \"status\": \"active\",\r\n          \"interpretation_services\": \"Interpretation services are available in Urdu, Polish, and Slovak\",\r\n          \"application_process\": \"If you are an NHS patient please ask your GP for a referral letter, we will then be in touch with you directly. If you are not an NHS patient you should ring our reception to arrange an appointment\",\r\n          \"fees_description\": \"Non-NHS patients are expected to pay for their counselling sessions. We charge a flat rate per hour of counselling. The current rate is \\u00a350 per hour. Please see our website for up to date prices.\",\r\n          \"wait_time\": \"wait_time\",\r\n          \"fees\": \"fees_description\",\r\n          \"accreditations\": \"All of our practitioners are accredited by the BASC, UKCP, and the Professional Standards Body\",\r\n          \"eligibility_description\": \"This service is intended for all people aged 12 and over who require counselling services in the MyCity area\",\r\n          \"minimum_age\": 12,\r\n          \"maximum_age\": 100,\r\n          \"assured_date\": \"2005-01-01\",\r\n          \"assurer_email\": \"email@example.com\",\r\n          \"licenses\": \"licences\",\r\n          \"alert\": \"Following COVID-19 we have moved most of our counselling sessions online. Please contact the reception if you require further information.\",\r\n          \"last_modified\": \"2023-03-15T10:30:45.123Z\",\r\n          \"organization\": {\r\n            \"id\": \"d9d5e0f5-d3ce-4f73-9a2f-4dd0ecc6c610\",\r\n            \"name\": \"Example Organization Inc.\",\r\n            \"alternate_name\": \"Example Org\",\r\n            \"description\": \"Example Org is a non-profit organization dedicated to providing services to qualified beneficiaries\",\r\n            \"email\": \"email@example.com\",\r\n            \"website\": \"http://example.com\",\r\n            \"tax_status\": \"tax_status\",\r\n            \"year_incorporated\": 2011,\r\n            \"legal_status\": \"Limited Company\",\r\n            \"logo\": \"https://openreferral.org/wp-content/uploads/2018/02/OpenReferral_Logo_Green-4-1.png\",\r\n            \"uri\": \"http://example.com\",\r\n            \"parent_organization_id\": \"cd09a387-91f4-4555-94ec-e799c35344cd\"\r\n          },\r\n          \"program\": {\r\n            \"id\": \"e7ec2e57-4540-43fa-b2c7-6be5a0ef7f42\",\r\n            \"name\": \"Community Mental Health Support\",\r\n            \"alternate_name\": \"MyCity Mental Health Group\",\r\n            \"description\": \"Comprehensive Mental Health Services available to residents of MyCity including CBT and Counselling. This is not an emergency service and should not be used as an alternative to hospital and GP services.\"\r\n          }\r\n        }\r\n      ]\r\n    }", null, 200 },
                    { 4, "getTaxonomyById", null, null, "", null, 200 },
                    { 5, "getPaginatedListOfTaxonomies", null, null, "", null, 200 },
                    { 6, "getPaginatedListOfTaxonomyTerms", null, null, "", null, 200 },
                    { 7, "getTaxonomyTermById", null, null, "", null, 200 },
                    { 8, "getOrganizationById", null, null, "", null, 200 },
                    { 9, "getPaginatedListOfOrganizations", null, null, "", null, 200 },
                    { 10, "getServiceAtLocationWithNestedDataById", null, null, "", null, 200 },
                    { 11, "getPaginatedListOfServiceAtLocation", null, null, "", null, 200 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MockResponses");
        }
    }
}
