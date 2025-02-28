BEGIN TRY
    BEGIN TRANSACTION
    
    DELETE FROM [dbo].[MockResponses]
    
    SET IDENTITY_INSERT [dbo].[MockResponses] ON 
           
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (1, N'getAPIMetaInformation', NULL, NULL, NULL, 200, N'{
      "version": "3.0",
      "profile": "https://todo/put/our/profile/uri/here",
      "openapi_url": "https://raw.githubusercontent.com/openreferral/specification/3.0/schema/openapi.json"
    }
    ')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (1001, N'getFullyNestedServiceById', NULL, N'id=ac148810-d857-441c-9679-408f346de14b', NULL, 200, N'{
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Community Counselling",
      "alternate_name": "MyCity Counselling Services",
      "description": "Counselling Services provided by trained professionals. Suitable for people with mental health conditions such as anxiety, depression, or eating disorders as well as people experiencing difficult life events and circumstances. ",
      "url": "http://example.com/counselling",
      "email": "email@example.com",
      "status": "active",
      "interpretation_services": "Interpretation services are available in Urdu, Polish, and Slovak",
      "application_process": "If you are an NHS patient please ask your GP for a referral letter, we will then be in touch with you directly. If you are not an NHS patient you should ring our reception to arrange an appointment",
      "fees_description": "Non-NHS patients are expected to pay for their counselling sessions. We charge a flat rate per hour of counselling. The current rate is \u00a350 per hour. Please see our website for up to date prices.",
      "wait_time": "wait_time",
      "fees": "fees_description",
      "accreditations": "All of our practitioners are accredited by the BASC, UKCP, and the Professional Standards Body",
      "eligibility_description": "This service is intended for all people aged 12 and over who require counselling services in the MyCity area",
      "minimum_age": 12,
      "maximum_age": 100,
      "assured_date": "2005-01-01",
      "assurer_email": "email@example.com",
      "licenses": "licences",
      "alert": "Following COVID-19 we have moved most of our counselling sessions online. Please contact the reception if you require further information.",
      "last_modified": "2023-03-15T10:30:45.123Z",
      "phones": [
        {
          "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
          "number": "\"+44 1234 234567\"",
          "extension": 100,
          "type": "voice",
          "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
          "languages": [
            {
              "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
              "name": "Urdu",
              "code": "ur",
              "note": "Translation services provided via bilingual staff when they are available"
            }
          ]
        }
      ],
      "schedules": [
        {
          "id": "48102e86-bb50-41c4-8f1e-e269368c41d1",
          "valid_from": "2005-01-01",
          "valid_to": "2005-01-01",
          "dtstart": "2005-01-01",
          "timezone": 3,
          "until": "2005-01-01",
          "count": 3,
          "wkst": "TU",
          "freq": "WEEKLY",
          "interval": 1,
          "byday": "TH,FR",
          "byweekno": "41,42",
          "bymonthday": "2,3,4",
          "byyearday": "2,3,4",
          "description": "The service is available from 10 am to 6pm weekdays. The service is not available on public holidays.",
          "opens_at": "10:00:00",
          "closes_at": "18:00:00",
          "schedule_link": "http://example.com",
          "attending_type": "You can attend this service in-person or remotely via video conferencing",
          "notes": "Some of our staff will be unavailable during their lunch break which may be any 30 minute period between 1200 and 1400"
        }
      ],
      "service_areas": [
        {
          "id": "381c64f1-a724-4884-9c21-ac96c21cca3e",
          "name": "The service is available to all residents of the MyCity area. We provide the service in our offices or via video conferencing to any who can provide evidence of their residency.",
          "description": "The service is available to all residents of the MyCity area, including all postcodes starting with AB1, AB2, and AB3",
          "extent": "extent",
          "extent_type": "geojson",
          "uri": "http://example.com"
        }
      ],
      "service_at_locations": [
        {
          "id": "e94c9f38-1e8f-4564-91d4-d53501ab1765",
          "description": "Counselling Services provided by trained professionals in the MyCity area.",
          "contacts": [
            {
              "id": "1e7efce3-639b-4880-940c-b95cd30cdb50",
              "name": "Ann Persona",
              "title": "Reception Manager",
              "department": "Reception",
              "email": "email@example.com",
              "phones": [
                {
                  "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
                  "number": "\"+44 1234 234567\"",
                  "extension": 100,
                  "type": "voice",
                  "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
                  "languages": [
                    {
                      "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                      "name": "Urdu",
                      "code": "ur",
                      "note": "Translation services provided via bilingual staff when they are available"
                    }
                  ]
                }
              ]
            }
          ],
          "phones": [
            {
              "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
              "number": "\"+44 1234 234567\"",
              "extension": 100,
              "type": "voice",
              "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
              "languages": [
                {
                  "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                  "name": "Urdu",
                  "code": "ur",
                  "note": "Translation services provided via bilingual staff when they are available"
                }
              ]
            }
          ],
          "schedules": [
            {
              "id": "48102e86-bb50-41c4-8f1e-e269368c41d1",
              "valid_from": "2005-01-01",
              "valid_to": "2005-01-01",
              "dtstart": "2005-01-01",
              "timezone": 3,
              "until": "2005-01-01",
              "count": 3,
              "wkst": "TU",
              "freq": "WEEKLY",
              "interval": 1,
              "byday": "TH,FR",
              "byweekno": "41,42",
              "bymonthday": "2,3,4",
              "byyearday": "2,3,4",
              "description": "The service is available from 10 am to 6pm weekdays. The service is not available on public holidays.",
              "opens_at": "10:00:00",
              "closes_at": "18:00:00",
              "schedule_link": "http://example.com",
              "attending_type": "You can attend this service in-person or remotely via video conferencing",
              "notes": "Some of our staff will be unavailable during their lunch break which may be any 30 minute period between 1200 and 1400"
            }
          ],
          "location": {
            "id": "3a19ff88-4620-4d17-9830-ac1d859eb5d5",
            "location_type": "physical",
            "url": "http://example.com",
            "name": "MyCity Civic Center",
            "alternate_name": "Civic Center",
            "description": "MyCity Civic Center is located on Main Street and contains facilities for a number of civic and community services available.",
            "transportation": "MyCity Civic Center is serviced by the number 1 Bus and is a short walk from the Main Street Metro Station",
            "latitude": 100,
            "longitude": 101,
            "external_identifier": "10092008082",
            "external_identifier_type": "UPRN",
            "languages": [
              {
                "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                "name": "Urdu",
                "code": "ur",
                "note": "Translation services provided via bilingual staff when they are available"
              }
            ],
            "addresses": [
              {
                "id": "74706e55-df26-4b84-80fe-ecc30b5befb4",
                "attention": "A. Persona",
                "address_1": "1-30 Main Street",
                "address_2": "MyVillage",
                "city": "MyCity",
                "region": "MyRegion",
                "state_province": "MyState",
                "postal_code": "ABC 1234",
                "country": "US",
                "address_type": "postal"
              }
            ],
            "contacts": [
              {
                "id": "1e7efce3-639b-4880-940c-b95cd30cdb50",
                "name": "Ann Persona",
                "title": "Reception Manager",
                "department": "Reception",
                "email": "email@example.com",
                "phones": [
                  {
                    "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
                    "number": "\"+44 1234 234567\"",
                    "extension": 100,
                    "type": "voice",
                    "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
                    "languages": [
                      {
                        "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                        "name": "Urdu",
                        "code": "ur",
                        "note": "Translation services provided via bilingual staff when they are available"
                      }
                    ]
                  }
                ]
              }
            ],
            "accessibility": [
              {
                "id": "afcf296e-1cb2-4139-9c88-33d587d1a50b",
                "description": "The building is equipped with induction loops for hearing aids. Wheelchair access is possible on all levels.",
                "details": "Switch hearing aid to T-coil to access the induction loop facility.",
                "url": "http://example.com"
              }
            ],
            "phones": [
              {
                "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
                "number": "\"+44 1234 234567\"",
                "extension": 100,
                "type": "voice",
                "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
                "languages": [
                  {
                    "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                    "name": "Urdu",
                    "code": "ur",
                    "note": "Translation services provided via bilingual staff when they are available"
                  }
                ]
              }
            ],
            "schedules": [
              {
                "id": "48102e86-bb50-41c4-8f1e-e269368c41d1",
                "valid_from": "2005-01-01",
                "valid_to": "2005-01-01",
                "dtstart": "2005-01-01",
                "timezone": 3,
                "until": "2005-01-01",
                "count": 3,
                "wkst": "TU",
                "freq": "WEEKLY",
                "interval": 1,
                "byday": "TH,FR",
                "byweekno": "41,42",
                "bymonthday": "2,3,4",
                "byyearday": "2,3,4",
                "description": "The service is available from 10 am to 6pm weekdays. The service is not available on public holidays.",
                "opens_at": "10:00:00",
                "closes_at": "18:00:00",
                "schedule_link": "http://example.com",
                "attending_type": "You can attend this service in-person or remotely via video conferencing",
                "notes": "Some of our staff will be unavailable during their lunch break which may be any 30 minute period between 1200 and 1400"
              }
            ]
          }
        }
      ],
      "languages": [
        {
          "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
          "name": "Urdu",
          "code": "ur",
          "note": "Translation services provided via bilingual staff when they are available"
        }
      ],
      "organization": {
        "id": "d9d5e0f5-d3ce-4f73-9a2f-4dd0ecc6c610",
        "name": "Example Organization Inc.",
        "alternate_name": "Example Org",
        "description": "Example Org is a non-profit organization dedicated to providing services to qualified beneficiaries",
        "email": "email@example.com",
        "website": "http://example.com",
        "tax_status": "tax_status",
        "year_incorporated": 2011,
        "legal_status": "Limited Company",
        "logo": "https://openreferral.org/wp-content/uploads/2018/02/OpenReferral_Logo_Green-4-1.png",
        "uri": "http://example.com",
        "parent_organization_id": "cd09a387-91f4-4555-94ec-e799c35344cd",
        "funding": [
          {
            "id": "1f2df32c-bf08-4b8e-bd6f-e834014b19bc",
            "source": "This service is funded partially by the MyCity local government and via grants made by charity funding bodies. We also operate a for-profit department which is used to partially cover the expense of the public service"
          }
        ],
        "contacts": [
          {
            "id": "1e7efce3-639b-4880-940c-b95cd30cdb50",
            "name": "Ann Persona",
            "title": "Reception Manager",
            "department": "Reception",
            "email": "email@example.com",
            "phones": [
              {
                "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
                "number": "\"+44 1234 234567\"",
                "extension": 100,
                "type": "voice",
                "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
                "languages": [
                  {
                    "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                    "name": "Urdu",
                    "code": "ur",
                    "note": "Translation services provided via bilingual staff when they are available"
                  }
                ]
              }
            ]
          }
        ],
        "phones": [
          {
            "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
            "number": "\"+44 1234 234567\"",
            "extension": 100,
            "type": "voice",
            "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
            "languages": [
              {
                "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                "name": "Urdu",
                "code": "ur",
                "note": "Translation services provided via bilingual staff when they are available"
              }
            ]
          }
        ],
        "locations": [
          {
            "id": "3a19ff88-4620-4d17-9830-ac1d859eb5d5",
            "location_type": "physical",
            "url": "http://example.com",
            "name": "MyCity Civic Center",
            "alternate_name": "Civic Center",
            "description": "MyCity Civic Center is located on Main Street and contains facilities for a number of civic and community services available.",
            "transportation": "MyCity Civic Center is serviced by the number 1 Bus and is a short walk from the Main Street Metro Station",
            "latitude": 100,
            "longitude": 101,
            "external_identifier": "10092008082",
            "external_identifier_type": "UPRN",
            "languages": [
              {
                "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                "name": "Urdu",
                "code": "ur",
                "note": "Translation services provided via bilingual staff when they are available"
              }
            ],
            "addresses": [
              {
                "id": "74706e55-df26-4b84-80fe-ecc30b5befb4",
                "attention": "A. Persona",
                "address_1": "1-30 Main Street",
                "address_2": "MyVillage",
                "city": "MyCity",
                "region": "MyRegion",
                "state_province": "MyState",
                "postal_code": "ABC 1234",
                "country": "US",
                "address_type": "postal"
              }
            ],
            "contacts": [
              {
                "id": "1e7efce3-639b-4880-940c-b95cd30cdb50",
                "name": "Ann Persona",
                "title": "Reception Manager",
                "department": "Reception",
                "email": "email@example.com",
                "phones": [
                  {
                    "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
                    "number": "\"+44 1234 234567\"",
                    "extension": 100,
                    "type": "voice",
                    "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
                    "languages": [
                      {
                        "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                        "name": "Urdu",
                        "code": "ur",
                        "note": "Translation services provided via bilingual staff when they are available"
                      }
                    ]
                  }
                ]
              }
            ],
            "accessibility": [
              {
                "id": "afcf296e-1cb2-4139-9c88-33d587d1a50b",
                "description": "The building is equipped with induction loops for hearing aids. Wheelchair access is possible on all levels.",
                "details": "Switch hearing aid to T-coil to access the induction loop facility.",
                "url": "http://example.com"
              }
            ],
            "phones": [
              {
                "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
                "number": "\"+44 1234 234567\"",
                "extension": 100,
                "type": "voice",
                "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
                "languages": [
                  {
                    "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                    "name": "Urdu",
                    "code": "ur",
                    "note": "Translation services provided via bilingual staff when they are available"
                  }
                ]
              }
            ],
            "schedules": [
              {
                "id": "48102e86-bb50-41c4-8f1e-e269368c41d1",
                "valid_from": "2005-01-01",
                "valid_to": "2005-01-01",
                "dtstart": "2005-01-01",
                "timezone": 3,
                "until": "2005-01-01",
                "count": 3,
                "wkst": "TU",
                "freq": "WEEKLY",
                "interval": 1,
                "byday": "TH,FR",
                "byweekno": "41,42",
                "bymonthday": "2,3,4",
                "byyearday": "2,3,4",
                "description": "The service is available from 10 am to 6pm weekdays. The service is not available on public holidays.",
                "opens_at": "10:00:00",
                "closes_at": "18:00:00",
                "schedule_link": "http://example.com",
                "attending_type": "You can attend this service in-person or remotely via video conferencing",
                "notes": "Some of our staff will be unavailable during their lunch break which may be any 30 minute period between 1200 and 1400"
              }
            ]
          }
        ],
        "programs": [
          {
            "id": "e7ec2e57-4540-43fa-b2c7-6be5a0ef7f42",
            "name": "Community Mental Health Support",
            "alternate_name": "MyCity Mental Health Group",
            "description": "Comprehensive Mental Health Services available to residents of MyCity including CBT and Counselling. This is not an emergency service and should not be used as an alternative to hospital and GP services."
          }
        ],
        "organization_identifiers": [
          {
            "id": "d4dbcebc-0802-47cb-8651-b937ac4f2f3e",
            "identifier_scheme": "GB-COH",
            "identifier_type": "Company number",
            "identifier": "1234567"
          }
        ]
      },
      "funding": [
        {
          "id": "1f2df32c-bf08-4b8e-bd6f-e834014b19bc",
          "source": "This service is funded partially by the MyCity local government and via grants made by charity funding bodies. We also operate a for-profit department which is used to partially cover the expense of the public service"
        }
      ],
      "cost_options": [
        {
          "id": "1fdf4d39-3d80-484d-9f92-a8ffa08621e7",
          "valid_from": "2020-01-01",
          "valid_to": "2022-12-31",
          "option": "Under 18s and Over 60s",
          "currency": "gbp",
          "amount": 35,
          "amount_description": "per 1 hour session"
        }
      ],
      "program": {
        "id": "e7ec2e57-4540-43fa-b2c7-6be5a0ef7f42",
        "name": "Community Mental Health Support",
        "alternate_name": "MyCity Mental Health Group",
        "description": "Comprehensive Mental Health Services available to residents of MyCity including CBT and Counselling. This is not an emergency service and should not be used as an alternative to hospital and GP services."
      },
      "required_documents": [
        {
          "id": "f6ad7e69-b9c8-42ce-92db-92cedb4c05c0",
          "document": "Any official identification document (Passport, Driver''s Licence, identity card) and evidence of address such as an official letter from local or national government. A driver''s license which shows your name, photograph, and address can be used as a sole identification document.",
          "uri": "http://example.com"
        }
      ],
      "contacts": [
        {
          "id": "1e7efce3-639b-4880-940c-b95cd30cdb50",
          "name": "Ann Persona",
          "title": "Reception Manager",
          "department": "Reception",
          "email": "email@example.com",
          "phones": [
            {
              "id": "1554f2e2-a373-45db-a3fa-9fc48a61c15e",
              "number": "\"+44 1234 234567\"",
              "extension": 100,
              "type": "voice",
              "description": "Our main reception phone number. The phones will be available from 0800 (8am) until 1800 (6pm) local time. There may be some slight delays in answering your call if we are busy.",
              "languages": [
                {
                  "id": "2989d3ed-c547-48f8-8f9d-432d81c7892e",
                  "name": "Urdu",
                  "code": "ur",
                  "note": "Translation services provided via bilingual staff when they are available"
                }
              ]
            }
          ]
        }
      ],
      "attributes": [
        {
          "id": "ae58cc39-8b70-4ab1-8aea-786882e5ac8e",
          "link_type": "link_type",
          "link_entity": "link_entity",
          "value": "value",
          "taxonomy_term": {
            "id": "3f7b145d-84af-42d7-8fae-eaca714b02b2",
            "code": "code",
            "name": "name",
            "description": "description",
            "parent_id": "0bc248fa-dc27-4650-9ba4-8f1a24ef16a2",
            "taxonomy": "taxonomy",
            "taxonomy_detail": {
              "id": "5c4d79d7-cc55-470e-9f1f-8cad074e4892",
              "name": "name",
              "description": "description",
              "uri": "http://example.com",
              "version": "version"
            },
            "language": "eng",
            "term_uri": "http://example.com"
          }
        }
      ],
      "metadata": [
        {
          "id": "6cd71e9f-1013-49d9-8370-26b8f59d3e5a",
          "resource_type": "location",
          "last_action_date": "2011-01-01",
          "last_action_type": "update",
          "field_name": "name",
          "previous_value": "MyCity Civic Center",
          "replacement_value": "MyCity New Civic Center",
          "updated_by": "Ann Persona"
        }
      ]
    }')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (2001, N'getPaginatedListOfServices', NULL, NULL, NULL, 200, N'    {
          "total_items": 1,
          "total_pages": 1,
          "page_number": 1,
          "size": 1,
          "first_page": true,
          "last_page": false,
          "empty": false,
          "contents": [
            {
              "id": "ac148810-d857-441c-9679-408f346de14b",
              "name": "Community Counselling",
              "alternate_name": "MyCity Counselling Services",
              "description": "Counselling Services provided by trained professionals. Suitable for people with mental health conditions such as anxiety, depression, or eating disorders as well as people experiencing difficult life events and circumstances. ",
              "url": "http://example.com/counselling",
              "email": "email@example.com",
              "status": "active",
              "interpretation_services": "Interpretation services are available in Urdu, Polish, and Slovak",
              "application_process": "If you are an NHS patient please ask your GP for a referral letter, we will then be in touch with you directly. If you are not an NHS patient you should ring our reception to arrange an appointment",
              "fees_description": "Non-NHS patients are expected to pay for their counselling sessions. We charge a flat rate per hour of counselling. The current rate is \u00a350 per hour. Please see our website for up to date prices.",
              "wait_time": "wait_time",
              "fees": "fees_description",
              "accreditations": "All of our practitioners are accredited by the BASC, UKCP, and the Professional Standards Body",
              "eligibility_description": "This service is intended for all people aged 12 and over who require counselling services in the MyCity area",
              "minimum_age": 12,
              "maximum_age": 100,
              "assured_date": "2005-01-01",
              "assurer_email": "email@example.com",
              "licenses": "licences",
              "alert": "Following COVID-19 we have moved most of our counselling sessions online. Please contact the reception if you require further information.",
              "last_modified": "2023-03-15T10:30:45.123Z",
              "organization": {
                "id": "d9d5e0f5-d3ce-4f73-9a2f-4dd0ecc6c610",
                "name": "Example Organization Inc.",
                "alternate_name": "Example Org",
                "description": "Example Org is a non-profit organization dedicated to providing services to qualified beneficiaries",
                "email": "email@example.com",
                "website": "http://example.com",
                "tax_status": "tax_status",
                "year_incorporated": 2011,
                "legal_status": "Limited Company",
                "logo": "https://openreferral.org/wp-content/uploads/2018/02/OpenReferral_Logo_Green-4-1.png",
                "uri": "http://example.com",
                "parent_organization_id": "cd09a387-91f4-4555-94ec-e799c35344cd"
              },
              "program": {
                "id": "e7ec2e57-4540-43fa-b2c7-6be5a0ef7f42",
                "name": "Community Mental Health Support",
                "alternate_name": "MyCity Mental Health Group",
                "description": "Comprehensive Mental Health Services available to residents of MyCity including CBT and Counselling. This is not an emergency service and should not be used as an alternative to hospital and GP services."
              }
            }
          ]
        }')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (2002, N'getPaginatedListOfServices', N'Pagination', NULL, NULL, 200, N'    {
          "total_items": 1,
          "total_pages": 1,
          "page_number": 1,
          "size": 1,
          "first_page": true,
          "last_page": false,
          "empty": false,
          "contents": [
            {
              "id": "ac148810-d857-441c-9679-408f346de14b",
              "name": "Service 1"
            },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 2"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 3"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 4"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 5"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 6"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 7"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 8"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 9"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 10"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 11"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 12"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 13"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 14"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 15"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 16"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 17"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 18"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 19"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 20"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 21"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 22"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 23"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 24"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 25"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 26"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 27"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 28"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 29"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 30"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 31"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 32"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 33"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 34"
    },
    {
      "id": "ac148810-d857-441c-9679-408f346de14b",
      "name": "Service 35"
    }
          ]
        }')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (2003, N'getPaginatedListOfServices', N'Empty', NULL, NULL, 200, N'    {
          "total_items": 1,
          "total_pages": 1,
          "page_number": 1,
          "size": 1,
          "first_page": true,
          "last_page": false,
          "empty": false,
          "contents": [
          ]
        }')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (2004, N'getPaginatedListOfServices', N'500', NULL, NULL, 500, N'')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (3001, N'getTaxonomyById', NULL, NULL, NULL, 200, N'')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (4001, N'getPaginatedListOfTaxonomies', NULL, NULL, NULL, 200, N'')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (5001, N'getPaginatedListOfTaxonomyTerms', NULL, NULL, NULL, 200, N'')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (6001, N'getTaxonomyTermById', NULL, NULL, NULL, 200, N'')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (7001, N'getOrganizationById', NULL, NULL, NULL, 200, N'')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (8001, N'getPaginatedListOfOrganizations', NULL, NULL, NULL, 200, N'')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (9001, N'getServiceAtLocationWithNestedDataById', NULL, NULL, NULL, 200, N'')
    INSERT [dbo].[MockResponses] ([Id], [OperationName], [ScenarioName], [PathParams], [QueryParams], [StatusCode], [ResponseBody]) VALUES (10001, N'getPaginatedListOfServiceAtLocation', NULL, NULL, NULL, 200, N'')
    
    SET IDENTITY_INSERT [dbo].[MockResponses] OFF
    
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage
    
    DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;
    
    SELECT
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE(),
        @ErrorMessage = ERROR_MESSAGE()

    ROLLBACK TRANSACTION
    
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
END CATCH