using System.Text.Json;
using FamilyHubs.SharedKernel.OpenReferral.Entities;

namespace FamilyHubs.OpenReferral.UnitTests.Helpers;

public static class MockService
{
    public static Service Service { get; } = new()
    {
        OrId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
        Name = "Service Name",
        Status = "Active",
        Program = new Program
        {
            OrId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
        },
        Organization = new Organization
        {
            OrId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
            Name = "Organization Name",
            Description = "Organization Description",
            YearIncorporated = 2024,
            LegalStatus = "LegalStatus",
            Funding =
            [
                new Funding
                {
                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                }
            ],
            Contacts =
            [
                new Contact
                {
                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                }
            ],
            Phones =
            [
                new Phone
                {
                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Number = "01234567890",
                    Extension = 1,
                    Languages =
                    [
                        new Language
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Name = "English",
                            Code = "en",
                        }
                    ]
                }
            ],
            Locations =
            [
                new Location
                {
                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    LocationType = "LocationType",
                    Languages =
                    [
                        new Language
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Name = "English",
                            Code = "en",
                        }
                    ],
                    Addresses =
                    [
                        new Address
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            Address1 = "100 Street",
                            City = "City",
                            StateProvince = "Province",
                            PostalCode = "AB1 2DE",
                            Country = "United Kingdom",
                            AddressType = "AddressType",
                        }
                    ],
                    Contacts =
                    [
                        new Contact
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        }
                    ],
                    Accessibilities =
                    [
                        new Accessibility
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                        }
                    ],
                    Phones =
                    [
                        new Phone
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            Number = "01234567890",
                            Extension = 1,
                            Languages =
                            [
                                new Language
                                {
                                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                                    Name = "English",
                                    Code = "en",
                                }
                            ]
                        }
                    ],
                    Schedules =
                    [
                        new Schedule
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000003")
                        }
                    ]
                },
            ],
            Programs =
            [
                new Program
                {
                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                }
            ],
            OrganizationIdentifiers =
            [
                new OrganizationIdentifier
                {
                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000014")
                }
            ]
        },
        Phones =
        [
            new Phone
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Number = "01234567890",
                Extension = 1,
                Languages =
                [
                    new Language
                    {
                        OrId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        Name = "English",
                        Code = "en",
                    }
                ]
            }
        ],
        Schedules =
        [
            new Schedule
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            }
        ],
        ServiceAreas =
        [
            new ServiceArea
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Name = "Service Area",
                Description = "Service Area"
            }
        ],
        ServiceAtLocations =
        [
            new ServiceAtLocation
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Location = new Location
                {
                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    LocationType = "LocationType",
                    Languages =
                    [
                        new Language
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Name = "English",
                            Code = "en",
                        }
                    ],
                    Addresses =
                    [
                        new Address
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            Address1 = "100 Street",
                            City = "City",
                            StateProvince = "Province",
                            PostalCode = "AB1 2DE",
                            Country = "United Kingdom",
                            AddressType = "AddressType",
                        }
                    ],
                    Contacts =
                    [
                        new Contact
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        }
                    ],
                    Accessibilities =
                    [
                        new Accessibility
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                        }
                    ],
                    Phones =
                    [
                        new Phone
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            Number = "01234567890",
                            Extension = 1,
                            Languages =
                            [
                                new Language
                                {
                                    OrId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                                    Name = "English",
                                    Code = "en",
                                }
                            ]
                        }
                    ],
                    Schedules =
                    [
                        new Schedule
                        {
                            OrId = Guid.Parse("00000000-0000-0000-0000-000000000003")
                        }
                    ]
                },
                Contacts =
                [
                    new Contact
                    {
                        OrId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    }
                ],
                Phones =
                [
                    new Phone
                    {
                        OrId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        Number = "01234567890",
                        Extension = 1,
                        Languages =
                        [
                            new Language
                            {
                                OrId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                                Name = "English",
                                Code = "en",
                            }
                        ]
                    }
                ],
                Schedules =
                [
                    new Schedule
                    {
                        OrId = Guid.Parse("00000000-0000-0000-0000-000000000003")
                    }
                ]
            }
        ],
        Languages =
        [
            new Language
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Name = "English",
                Code = "en",
            }
        ],
        Funding =
        [
            new Funding
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
            }
        ],
        CostOptions =
        [
            new CostOption
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
            }
        ],
        RequiredDocuments =
        [
            new RequiredDocument
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000012")
            }
        ],
        Contacts =
        [
            new Contact
            {
                OrId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
            }
        ],
    };

    public static string ServiceJson => JsonSerializer.Serialize(Service);
}