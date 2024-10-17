import { DataTypes } from 'sequelize'
import { serviceDirectoryDb } from '../connections.js'

// Model for Events
export const Events = serviceDirectoryDb.define(
  'Events',
  {
    Id: {
      type: DataTypes.SMALLINT,
      primaryKey: true,
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(100),
      allowNull: false
    },
    Description: {
      type: DataTypes.STRING(500)
    }
  },
  {
    tableName: 'Events',
    timestamps: false
  }
)

// Model for Organisations
export const Organisations = serviceDirectoryDb.define(
  'Organisations',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    OrganisationType: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(255),
      allowNull: false
    },
    Description: {
      type: DataTypes.STRING(500),
      allowNull: false
    },
    AdminAreaCode: {
      type: DataTypes.STRING(15),
      allowNull: false
    },
    AssociatedOrganisationId: {
      type: DataTypes.BIGINT
    },
    Logo: {
      type: DataTypes.STRING(2083)
    },
    Uri: {
      type: DataTypes.STRING(2083)
    },
    Url: {
      type: DataTypes.STRING(2083)
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    }
  },
  {
    tableName: 'Organisations',
    timestamps: false
  }
)

// Model for Locations
export const Locations = serviceDirectoryDb.define(
  'Locations',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    LocationTypeCategory: {
      type: DataTypes.STRING(9),
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(255)
    },
    Description: {
      type: DataTypes.STRING(1000)
    },
    Latitude: {
      type: DataTypes.FLOAT,
      allowNull: false
    },
    Longitude: {
      type: DataTypes.FLOAT,
      allowNull: false
    },
    Address1: {
      type: DataTypes.STRING(100),
      allowNull: false
    },
    Address2: {
      type: DataTypes.STRING(100)
    },
    City: {
      type: DataTypes.STRING(60),
      allowNull: false
    },
    PostCode: {
      type: DataTypes.STRING(15),
      allowNull: false
    },
    StateProvince: {
      type: DataTypes.STRING(60),
      allowNull: false
    },
    Country: {
      type: DataTypes.STRING(60),
      allowNull: false
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    AddressType: {
      type: DataTypes.STRING(10)
    },
    AlternateName: {
      type: DataTypes.STRING(255)
    },
    Attention: {
      type: DataTypes.STRING(255)
    },
    ExternalIdentifier: {
      type: DataTypes.STRING(500)
    },
    ExternalIdentifierType: {
      type: DataTypes.STRING(500)
    },
    LocationType: {
      type: DataTypes.STRING(8),
      allowNull: false
    },
    Region: {
      type: DataTypes.STRING(255)
    },
    Transportation: {
      type: DataTypes.STRING(500)
    },
    Url: {
      type: DataTypes.STRING(2083)
    },
    OrganisationId: {
      type: DataTypes.BIGINT,
      references: {
        model: Organisations,
        key: 'Id'
      }
    },
    GeoPoint: {
      type: DataTypes.GEOGRAPHY,
      allowNull: false
    }
  },
  {
    tableName: 'Locations',
    timestamps: false
  }
)

// Model for AccessibilityForDisabilities
export const AccessibilityForDisabilities = serviceDirectoryDb.define(
  'AccessibilityForDisabilities',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    Accessibility: {
      type: DataTypes.STRING(255)
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    LocationId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Locations,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'AccessibilityForDisabilities',
    timestamps: false
  }
)

// Model for ServiceTypes
export const ServiceTypes = serviceDirectoryDb.define(
  'ServiceTypes',
  {
    Id: {
      type: DataTypes.TINYINT,
      primaryKey: true,
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    Description: {
      type: DataTypes.STRING(255)
    }
  },
  {
    tableName: 'ServiceTypes',
    timestamps: false
  }
)

// Model for ServiceSearches
export const ServiceSearches = serviceDirectoryDb.define(
  'ServiceSearches',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    SearchTriggerEventId: {
      type: DataTypes.SMALLINT,
      allowNull: false,
      references: {
        model: Events,
        key: 'Id'
      }
    },
    SearchPostcode: {
      type: DataTypes.STRING(10),
      allowNull: false
    },
    SearchRadiusMiles: {
      type: DataTypes.TINYINT,
      allowNull: false
    },
    UserId: {
      type: DataTypes.BIGINT
    },
    HttpResponseCode: {
      type: DataTypes.SMALLINT
    },
    RequestTimestamp: {
      type: DataTypes.DATE,
      allowNull: false
    },
    ResponseTimestamp: {
      type: DataTypes.DATE
    },
    CorrelationId: {
      type: DataTypes.STRING(50)
    },
    ServiceSearchTypeId: {
      type: DataTypes.TINYINT,
      allowNull: false,
      references: {
        model: ServiceTypes,
        key: 'Id'
      }
    },
    OrganisationId: {
      type: DataTypes.BIGINT
    }
  },
  {
    tableName: 'ServiceSearches',
    timestamps: false
  }
)

// Model for Services
export const Services = serviceDirectoryDb.define(
  'Services',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    ServiceType: {
      type: DataTypes.STRING(18),
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(255),
      allowNull: false
    },
    Description: {
      type: DataTypes.TEXT
    },
    Status: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    Fees: {
      type: DataTypes.TEXT
    },
    Accreditations: {
      type: DataTypes.TEXT
    },
    DeliverableType: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    AssuredDate: {
      type: DataTypes.DATE
    },
    CanFamilyChooseDeliveryLocation: {
      type: DataTypes.BOOLEAN,
      allowNull: false
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    OrganisationId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Organisations,
        key: 'Id'
      }
    },
    InterpretationServices: {
      type: DataTypes.STRING(512)
    },
    Summary: {
      type: DataTypes.STRING(400)
    }
  },
  {
    tableName: 'Services',
    timestamps: false
  }
)

// Model for Contacts
export const Contacts = serviceDirectoryDb.define(
  'Contacts',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    Title: {
      type: DataTypes.STRING(50)
    },
    Name: {
      type: DataTypes.STRING(50)
    },
    Telephone: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    TextPhone: {
      type: DataTypes.STRING(50)
    },
    Url: {
      type: DataTypes.STRING(2083)
    },
    Email: {
      type: DataTypes.TEXT
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      references: {
        model: Services,
        key: 'Id'
      }
    },
    LocationId: {
      type: DataTypes.BIGINT,
      references: {
        model: Locations,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'Contacts',
    timestamps: false
  }
)

// Model for CostOptions
export const CostOptions = serviceDirectoryDb.define(
  'CostOptions',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    ValidFrom: {
      type: DataTypes.DATE
    },
    ValidTo: {
      type: DataTypes.DATE
    },
    Option: {
      type: DataTypes.STRING(20)
    },
    Amount: {
      type: DataTypes.DECIMAL(18, 2)
    },
    AmountDescription: {
      type: DataTypes.STRING(500)
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    },
    Currency: {
      type: DataTypes.CHAR(3)
    }
  },
  {
    tableName: 'CostOptions',
    timestamps: false
  }
)

// Model for Eligibilities
export const Eligibilities = serviceDirectoryDb.define(
  'Eligibilities',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    EligibilityType: {
      type: DataTypes.STRING(50)
    },
    MaximumAge: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    MinimumAge: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'Eligibilities',
    timestamps: false
  }
)

// Model for Fundings
export const Fundings = serviceDirectoryDb.define(
  'Fundings',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    Source: {
      type: DataTypes.STRING(255)
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'Fundings',
    timestamps: false
  }
)

// Model for Languages
export const Languages = serviceDirectoryDb.define(
  'Languages',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    Name: {
      type: DataTypes.STRING(100),
      allowNull: false
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    },
    Code: {
      type: DataTypes.STRING(3),
      defaultValue: '',
      allowNull: false
    },
    Note: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'Languages',
    timestamps: false
  }
)

// Model for ServiceAreas
export const ServiceAreas = serviceDirectoryDb.define(
  'ServiceAreas',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    ServiceAreaName: {
      type: DataTypes.STRING(255)
    },
    Extent: {
      type: DataTypes.STRING(255)
    },
    Uri: {
      type: DataTypes.STRING(2083)
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'ServiceAreas',
    timestamps: false
  }
)

// Model for ServiceAtLocations
export const ServiceAtLocations = serviceDirectoryDb.define(
  'ServiceAtLocations',
  {
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    },
    LocationId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Locations,
        key: 'Id'
      }
    },
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    Description: {
      type: DataTypes.TEXT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    }
  },
  {
    tableName: 'ServiceAtLocations',
    timestamps: false
  }
)

// Model for Schedules
export const Schedules = serviceDirectoryDb.define(
  'Schedules',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    OpensAt: {
      type: DataTypes.DATE
    },
    ClosesAt: {
      type: DataTypes.DATE
    },
    ValidFrom: {
      type: DataTypes.DATE
    },
    ValidTo: {
      type: DataTypes.DATE
    },
    DtStart: {
      type: DataTypes.STRING(30)
    },
    Freq: {
      type: DataTypes.STRING(8)
    },
    Interval: {
      type: DataTypes.INTEGER
    },
    ByDay: {
      type: DataTypes.STRING(34)
    },
    ByMonthDay: {
      type: DataTypes.STRING(15)
    },
    Description: {
      type: DataTypes.TEXT
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    },
    LocationId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Locations,
        key: 'Id'
      }
    },
    Timezone: {
      type: DataTypes.INTEGER
    },
    AttendingType: {
      type: DataTypes.TEXT
    },
    ByWeekNo: {
      type: DataTypes.STRING(300)
    },
    ByYearDay: {
      type: DataTypes.STRING(300)
    },
    Count: {
      type: DataTypes.INTEGER
    },
    Notes: {
      type: DataTypes.TEXT
    },
    ScheduleLink: {
      type: DataTypes.STRING(600)
    },
    Until: {
      type: DataTypes.STRING(300)
    },
    WkSt: {
      type: DataTypes.STRING(300)
    },
    ServiceAtLocationId: {
      type: DataTypes.BIGINT,
      references: {
        model: ServiceAtLocations,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'Schedules',
    timestamps: false
  }
)

// Model for ServiceDeliveries
export const ServiceDeliveries = serviceDirectoryDb.define(
  'ServiceDeliveries',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    Name: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'ServiceDeliveries',
    timestamps: false
  }
)

// Model for ServiceSearchResults
export const ServiceSearchResults = serviceDirectoryDb.define(
  'ServiceSearchResults',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    },
    ServiceSearchId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: ServiceSearches,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'ServiceSearchResults',
    timestamps: false
  }
)

// Model for Taxonomies
export const Taxonomies = serviceDirectoryDb.define(
  'Taxonomies',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    Name: {
      type: DataTypes.STRING(255),
      allowNull: false
    },
    TaxonomyType: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    ParentId: {
      type: DataTypes.BIGINT
    },
    Created: {
      type: DataTypes.DATE
    },
    CreatedBy: {
      type: DataTypes.BIGINT
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.BIGINT
    }
  },
  {
    tableName: 'Taxonomies',
    timestamps: false
  }
)

// Model for ServiceTaxonomies
export const ServiceTaxonomies = serviceDirectoryDb.define(
  'ServiceTaxonomies',
  {
    ServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Services,
        key: 'Id'
      }
    },
    TaxonomyId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Taxonomies,
        key: 'Id'
      }
    }
  },
  {
    tableName: 'ServiceTaxonomies',
    timestamps: false,
    primaryKey: ['ServiceId', 'TaxonomyId']
  }
)
