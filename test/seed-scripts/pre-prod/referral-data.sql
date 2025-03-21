BEGIN TRY
    BEGIN TRANSACTION
      
    DECLARE @Today DATETIME2
    SET @Today = GETDATE()
              
    DELETE FROM [dbo].[Recipients]
                 
    SET IDENTITY_INSERT [dbo].[Recipients] ON 
    
    INSERT INTO [dbo].[Recipients] ([Id], [Name], [Email], [Telephone], [TextPhone], [AddressLine1], [AddressLine2], [TownOrCity], [County], [PostCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, N'eewcqn81pbN5yFuyQK2Ykw==', N'nhmNq4i1+tIcmjnC5mfAhQ==', NULL, NULL, NULL, NULL, NULL, NULL, NULL, @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk='),
            (2, N'aQFCQWlATpKgUIVDsEzfKQ==', NULL, N'zkO2FzEyeXn2QLGXK8sMPA==', NULL, NULL, NULL, NULL, NULL, NULL, @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q='),
            (3, N'DQ1zKmkzD4hcKR2Lk4ih/A==', NULL, NULL, N'cL7Dxp91ZoLm0BGEU9Jt5w==', NULL, NULL, NULL, NULL, NULL, @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q='),
            (4, N'KsN+IpP68r73CZattWf0HQ==', NULL, NULL, NULL, N'HRYo3pjBzD93jQtsa1R+GMH19fnQieJJiCPF0wE60uM=', N'JPkvRDh3aQX8XbZMm5tytxGgypnaXnHxF+wiM+wzoGk=', N'giQhddUvVg+4PMcW/1DKnA==', NULL, N'8/b3Gyl/gRiPmhqqbybznA==', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=');
    
    SET IDENTITY_INSERT [dbo].[Recipients] OFF
    
    DELETE FROM [dbo].[Organisations]
           
    INSERT INTO [dbo].[Organisations] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (11, N'Elop Mentoring', N'Elop Mentoring', @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=');
    
    DELETE FROM [dbo].[ReferralServices]
    
    INSERT INTO [dbo].[ReferralServices] ([Id], [Name], [Description], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [OrganizationId])
    VALUES  (1, N'LGBT+ Asylum support', NULL, NULL, @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', 11),
            (3, N'Young People’s Services ‘Youth Out East’', N'In addition to operating direct community frontline services ELOP also delivers second-tier work which includes providing information, training, consultancy and support to statutory and voluntary sector policy makers, managers, service providers and their staff teams.', NULL, @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', 11);
    
    DELETE FROM [dbo].[Statuses]
    
    INSERT INTO [dbo].[Statuses] ([Id], [Name], [SortOrder], [SecondrySortOrder], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, N'New', 0, 1, @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (2, N'Opened', 1, 1, @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (3, N'Accepted', 2, 2, @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (4, N'Declined', 3, 0, @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==');
    
    DELETE FROM [dbo].[UserAccounts]
    
    INSERT INTO [dbo].[UserAccounts] ([Id], [EmailAddress], [Name], [PhoneNumber], [Team], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (5, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', N'LDwfLwvZInCYtLTEjFvXbg==', NULL, NULL, @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q='),
            (8, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', N'lN9EBiP2tViCAW+HN4jRyw==', NULL, NULL, @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=');
    
    DELETE FROM [dbo].[Referrals]
    
    SET IDENTITY_INSERT [dbo].[Referrals] ON 
    
    INSERT INTO [dbo].[Referrals] ([Id], [ReferrerTelephone], [ReasonForSupport], [EngageWithFamily], [ReasonForDecliningSupport], [StatusId], [RecipientId], [UserAccountId], [ReferralServiceId], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, NULL, N'kbNS/3wDVBNvHnIJEYBOaJPM/368H4dKJASSx9PdVYntfOLrkJvs6zvdWsUXR06/', N'ODfWGH38LGu1QihC4oTEU2pA/J2FtCkW08MDZzoOsSas4KF1meBZlNUd5IDqreV0KfTG4zO6KAH29pFntCrNOLBCTTZ+I0UWBKbVm/PUgmg=', NULL, 3, 1, 8, 1, @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', @Today, N'AZAyytrwm24KKGwOZQiFTlLCB6wBKgTM90MlRhtLTAA='),
            (2, NULL, N'xSEmaLrV00+U5Kwq/r3SBbn+XZR1p5Op8sSkfQgO3GYsO6bDVnq5UqhTCNfesswF', N'tR6bbzaOygKet+HyQDJkRA4LkhINUZRj5TqnHyDV+NS2e6keQj9flQ5OK8QmSodE', NULL, 1, 2, 5, 1, @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q='),
            (3, NULL, N'eCX4BqkeGgNqZgSZtFRxR2sQIa3AjZYxL58dhSl90evJPThOoLtDvI1tM15Y6bg5', N'keHNJJ3Q+t8sFy/K/FxUc7nmkzSHdaUHD4fpyZn+OQH0DbkMGqHSXuinZtlJNtCM', NULL, 1, 3, 5, 3, @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q='),
            (4, NULL, N'gL51GQTJ/E2NROeD1KzM2wyyWMC1+mVgeJL6coat1Yo=', N'AR2NEqKbwWy8estKq5v2smLHtMdWLIHCazqdQNnX1/c=', NULL, 1, 4, 5, 1, @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=');
    
    SET IDENTITY_INSERT [dbo].[Referrals] OFF
    
    DELETE FROM [dbo].[Roles]
    
    INSERT INTO [dbo].[Roles] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, N'DfeAdmin', N'DfE Administrator', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (2, N'LaManager', N'Local Authority Manager', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (3, N'VcsManager', N'VCS Manager', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (4, N'LaProfessional', N'Local Authority Professional', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (5, N'VcsProfessional', N'VCS Professional', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (6, N'LaDualRole', N'Local Authority Dual Role', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA=='),
            (7, N'VcsDualRole', N'VCS Dual Role', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==', @Today, N'xts6B+cxoSMJJuqbjZ6hXA==');
    
    DELETE FROM [dbo].[UserAccountRoles]
    
    SET IDENTITY_INSERT [dbo].[UserAccountRoles] ON 
    
    INSERT INTO [dbo].[UserAccountRoles] ([Id], [UserAccountId], [RoleId], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, 8, 6, @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk='),
            (2, 5, 6, @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=');
    
    SET IDENTITY_INSERT [dbo].[UserAccountRoles] OFF
    
    DELETE FROM [dbo].[ConnectionRequestsSentMetric]
    
    SET IDENTITY_INSERT [dbo].[ConnectionRequestsSentMetric] ON 
    
    INSERT INTO [dbo].[ConnectionRequestsSentMetric] ([Id], [LaOrganisationId], [UserAccountId], [RequestTimestamp], [RequestCorrelationId], [ResponseTimestamp], [HttpResponseCode], [ConnectionRequestId], [ConnectionRequestReferenceCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [VcsOrganisationId])
    VALUES  (1, 6, 8, @Today, N'f24e16e5cd60cfb52c2efe15a6f878c7', @Today, 200, 1, N'000001', @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', @Today, N'PlIAIr4t8xGohN8Di4VisoDgZI8E9zC3+M6HJhJcuZk=', 11),
            (2, 6, 5, @Today, N'd72fb29dccb170cf23dd71b8c7d41d5a', @Today, 200, 2, N'000002', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', 11),
            (3, 6, 5, @Today, N'46632d5024c4ee355efdfe804755dc36', @Today, 200, 3, N'000003', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', 11),
            (4, 6, 5, @Today, N'20356e51c206c6fb5a545e93d1501ca9', @Today, 200, 4, N'000004', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', @Today, N'6gky/AQK3Nu6QIJ3gIZ7WWMu5ZghzfaeqI5Crxymo7Q=', 11);
    
    SET IDENTITY_INSERT [dbo].[ConnectionRequestsSentMetric] OFF
    
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
