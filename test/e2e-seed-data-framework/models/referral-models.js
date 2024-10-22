import { DataTypes } from 'sequelize'
import { referralDb } from '../connections.js'

// Model for ConnectCache
export const ConnectCache = referralDb.define(
  'ConnectCache',
  {
    Id: {
      type: DataTypes.STRING(449),
      primaryKey: true,
      allowNull: false
    },
    Value: {
      type: DataTypes.BLOB, // varbinary(max)
      allowNull: false
    },
    ExpiresAtTime: {
      type: DataTypes.DATE,
      allowNull: false
    },
    SlidingExpirationInSeconds: {
      type: DataTypes.BIGINT
    },
    AbsoluteExpiration: {
      type: DataTypes.DATE
    }
  },
  {
    tableName: 'ConnectCache',
    timestamps: false
  }
)

// Model for ConnectionRequestsSentMetric
export const ConnectionRequestsSentMetric = referralDb.define(
  'ConnectionRequestsSentMetric',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    LaOrganisationId: {
      type: DataTypes.BIGINT,
      allowNull: false
    },
    UserAccountId: {
      type: DataTypes.BIGINT,
      allowNull: false
    },
    RequestTimestamp: {
      type: DataTypes.DATE,
      allowNull: false
    },
    RequestCorrelationId: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    ResponseTimestamp: {
      type: DataTypes.DATE
    },
    HttpResponseCode: {
      type: DataTypes.SMALLINT
    },
    ConnectionRequestId: {
      type: DataTypes.BIGINT
    },
    ConnectionRequestReferenceCode: {
      type: DataTypes.CHAR(6)
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    },
    VcsOrganisationId: {
      type: DataTypes.BIGINT,
      defaultValue: 0,
      allowNull: false
    }
  },
  {
    tableName: 'ConnectionRequestsSentMetric',
    timestamps: false
  }
)

// Model for DataProtectionKeys
export const DataProtectionKeys = referralDb.define(
  'DataProtectionKeys',
  {
    Id: {
      type: DataTypes.INTEGER,
      primaryKey: true,
      autoIncrement: true
    },
    FriendlyName: {
      type: DataTypes.TEXT
    },
    Xml: {
      type: DataTypes.TEXT
    }
  },
  {
    tableName: 'DataProtectionKeys',
    timestamps: false
  }
)

// Model for Recipients
export const Recipients = referralDb.define(
  'Recipients',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    Name: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    Email: {
      type: DataTypes.STRING(512)
    },
    Telephone: {
      type: DataTypes.TEXT
    },
    TextPhone: {
      type: DataTypes.TEXT
    },
    AddressLine1: {
      type: DataTypes.TEXT
    },
    AddressLine2: {
      type: DataTypes.TEXT
    },
    TownOrCity: {
      type: DataTypes.TEXT
    },
    County: {
      type: DataTypes.TEXT
    },
    PostCode: {
      type: DataTypes.TEXT
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'Recipients',
    timestamps: false
  }
)

// Model for ReferralServices
export const ReferralServices = referralDb.define(
  'ReferralServices',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(256),
      allowNull: false
    },
    Description: {
      type: DataTypes.TEXT
    },
    Url: {
      type: DataTypes.TEXT
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'ReferralServices',
    timestamps: false
  }
)

// Model for Organisations
export const Organisations = referralDb.define(
  'Organisations',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      allowNull: false
    },
    ReferralServiceId: {
      type: DataTypes.BIGINT,
      references: {
        model: ReferralServices,
        key: 'Id'
      }
    },
    Name: {
      type: DataTypes.STRING(256),
      allowNull: false
    },
    Description: {
      type: DataTypes.TEXT
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'Organisations',
    timestamps: false
  }
)

// Model for Roles
export const Roles = referralDb.define(
  'Roles',
  {
    Id: {
      type: DataTypes.TINYINT,
      primaryKey: true,
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(256),
      allowNull: false
    },
    Description: {
      type: DataTypes.TEXT
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'Roles',
    timestamps: false
  }
)

// Model for Statuses
export const Statuses = referralDb.define(
  'Statuses',
  {
    Id: {
      type: DataTypes.TINYINT,
      primaryKey: true,
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(256),
      allowNull: false
    },
    SortOrder: {
      type: DataTypes.TINYINT,
      allowNull: false
    },
    SecondrySortOrder: {
      type: DataTypes.TINYINT,
      allowNull: false
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'Statuses',
    timestamps: false
  }
)

// Model for UserAccounts
export const UserAccounts = referralDb.define(
  'UserAccounts',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      allowNull: false
    },
    EmailAddress: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    PhoneNumber: {
      type: DataTypes.TEXT
    },
    Team: {
      type: DataTypes.TEXT
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'UserAccounts',
    timestamps: false
  }
)

// Model for Referrals
export const Referrals = referralDb.define(
  'Referrals',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    ReferrerTelephone: {
      type: DataTypes.TEXT
    },
    ReasonForSupport: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    EngageWithFamily: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    ReasonForDecliningSupport: {
      type: DataTypes.TEXT
    },
    StatusId: {
      type: DataTypes.TINYINT,
      allowNull: false,
      references: {
        model: Statuses,
        key: 'Id'
      }
    },
    RecipientId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Recipients,
        key: 'Id'
      }
    },
    UserAccountId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: UserAccounts,
        key: 'Id'
      }
    },
    ReferralServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: ReferralServices,
        key: 'Id'
      }
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'Referrals',
    timestamps: false
  }
)

// Model for UserAccountOrganisations
export const UserAccountOrganisations = referralDb.define(
  'UserAccountOrganisations',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    UserAccountId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: UserAccounts,
        key: 'Id'
      }
    },
    OrganisationId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: Organisations,
        key: 'Id'
      }
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'UserAccountOrganisations',
    timestamps: false
  }
)

// Model for UserAccountRoles
export const UserAccountRoles = referralDb.define(
  'UserAccountRoles',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    UserAccountId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: UserAccounts,
        key: 'Id'
      }
    },
    RoleId: {
      type: DataTypes.TINYINT,
      allowNull: false,
      references: {
        model: Roles,
        key: 'Id'
      }
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'UserAccountRoles',
    timestamps: false
  }
)

// Model for UserAccountServices
export const UserAccountServices = referralDb.define(
  'UserAccountServices',
  {
    Id: {
      type: DataTypes.BIGINT,
      primaryKey: true,
      autoIncrement: true
    },
    UserAccountId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: UserAccounts,
        key: 'Id'
      }
    },
    ReferralServiceId: {
      type: DataTypes.BIGINT,
      allowNull: false,
      references: {
        model: ReferralServices,
        key: 'Id'
      }
    },
    Created: {
      type: DataTypes.DATE,
      allowNull: false
    },
    CreatedBy: {
      type: DataTypes.STRING(512),
      allowNull: false
    },
    LastModified: {
      type: DataTypes.DATE
    },
    LastModifiedBy: {
      type: DataTypes.STRING(512)
    }
  },
  {
    tableName: 'UserAccountServices',
    timestamps: false
  }
)
