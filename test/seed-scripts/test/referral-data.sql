BEGIN TRY
    BEGIN TRANSACTION
          
    DECLARE @Today DATETIME2
    SET @Today = GETDATE()

    DELETE FROM [dbo].[Recipients]
                 
    SET IDENTITY_INSERT [dbo].[Recipients] ON

    INSERT [dbo].[Recipients] ([Id], [Name], [Email], [Telephone], [TextPhone], [AddressLine1], [AddressLine2], [TownOrCity], [County], [PostCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, N'J2fVxXMRBh39Evjy0GqNgg==', N'/Y/JApAE7ZvR70ss/rgz+A==', NULL, NULL, NULL, NULL, NULL, NULL, NULL, @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=')
    INSERT [dbo].[Recipients] ([Id], [Name], [Email], [Telephone], [TextPhone], [AddressLine1], [AddressLine2], [TownOrCity], [County], [PostCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, N'ttRxDkUVa6qogYNeLv0YxA==', NULL, N'2T8+EdSwp0z+l8uThHP2BA==', NULL, NULL, NULL, NULL, NULL, NULL, @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=')
    INSERT [dbo].[Recipients] ([Id], [Name], [Email], [Telephone], [TextPhone], [AddressLine1], [AddressLine2], [TownOrCity], [County], [PostCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, N'TQa9EL2qt5ggZI5n9ClyhA==', NULL, NULL, N'zeHsOU+sTxjsiQPmePTOsw==', NULL, NULL, NULL, NULL, NULL, @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=')
    INSERT [dbo].[Recipients] ([Id], [Name], [Email], [Telephone], [TextPhone], [AddressLine1], [AddressLine2], [TownOrCity], [County], [PostCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, N'LnYAcA1LUFsZ9obYCIvvRA==', NULL, NULL, NULL, N'Viz7+m0Rqnf46A/nuVUQmANcmmH1oHh3qz/whfBcCxY=', N'DGxO7MiQ2FTDPk2zHAG/9IMEXeIv0GOiGpMbHIRHLc4=', N'oIZIlZx6HoLvUFT+9+jrjA==', NULL, N'5gxMsdsRTMmjQascrQNeaA==', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=')
    
    SET IDENTITY_INSERT [dbo].[Recipients] OFF
    
    DELETE FROM [dbo].[Organisations]

    INSERT [dbo].[Organisations] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, N'Elop Mentoring', N'Elop Mentoring', @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=')
    
    DELETE FROM [dbo].[ReferralServices]

    INSERT [dbo].[ReferralServices] ([Id], [Name], [Description], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [OrganizationId]) VALUES (1, N'LGBT+ Asylum support', NULL, NULL, @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', 11)
    INSERT [dbo].[ReferralServices] ([Id], [Name], [Description], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [OrganizationId]) VALUES (3, N'Young People’s Services ‘Youth Out East’', N'In addition to operating direct community frontline services ELOP also delivers second-tier work which includes providing information, training, consultancy and support to statutory and voluntary sector policy makers, managers, service providers and their staff teams.', NULL, @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', 11)
    
    DELETE FROM [dbo].[Statuses]

    INSERT [dbo].[Statuses] ([Id], [Name], [SortOrder], [SecondrySortOrder], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, N'New', 0, 1, @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Statuses] ([Id], [Name], [SortOrder], [SecondrySortOrder], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, N'Opened', 1, 1, @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Statuses] ([Id], [Name], [SortOrder], [SecondrySortOrder], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, N'Accepted', 2, 2, @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Statuses] ([Id], [Name], [SortOrder], [SecondrySortOrder], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, N'Declined', 3, 0, @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    
    DELETE FROM [dbo].[UserAccounts]

    INSERT [dbo].[UserAccounts] ([Id], [EmailAddress], [Name], [PhoneNumber], [Team], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', N'azEgHm81eR9thPPfPfXS1g==', NULL, NULL, @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=')
    INSERT [dbo].[UserAccounts] ([Id], [EmailAddress], [Name], [PhoneNumber], [Team], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', N'HC07SyQtXWWRS0PDtMiFEw==', NULL, NULL, @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=')
    
    DELETE FROM [dbo].[Referrals]
    
    SET IDENTITY_INSERT [dbo].[Referrals] ON

    INSERT [dbo].[Referrals] ([Id], [ReferrerTelephone], [ReasonForSupport], [EngageWithFamily], [ReasonForDecliningSupport], [StatusId], [RecipientId], [UserAccountId], [ReferralServiceId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, NULL, N'ZQrpmYY4Hv6RVtj1dbfcBDgXpcf6Y46ieV7PH66p2Z6vTd21uXkQOvx3u3aT80mM', N'Yo2PK1ra3BjyqY1NyWOtgP1pnu5ZlIhoyrgZVzROeAh+JiVvPWT+eba1BIaN3sJFDE3uItVFyCjUYxHG7F7AYNwaeRULs0/1tb2xkU7TguA=', NULL, 3, 1, 8, 1, @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', @Today, N'4WgfGbK/qBbY+1/dNEVWiQIKX4uDFJT8JjElk5gefLs=')
    INSERT [dbo].[Referrals] ([Id], [ReferrerTelephone], [ReasonForSupport], [EngageWithFamily], [ReasonForDecliningSupport], [StatusId], [RecipientId], [UserAccountId], [ReferralServiceId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, NULL, N'jmk3nYGKZ9S7AE3TyG4PIyfmk9zc5kTFx4W8JxPwtvF1DUSVy8lFcKTwzYpgwDok', N'2zkCaX2aEnw2kjHxMMdJd0zp7X4Z+KmRSLiEgcjc7T71zt7wPNNRGKXB4meDvYnS', NULL, 1, 2, 5, 1, @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=')
    INSERT [dbo].[Referrals] ([Id], [ReferrerTelephone], [ReasonForSupport], [EngageWithFamily], [ReasonForDecliningSupport], [StatusId], [RecipientId], [UserAccountId], [ReferralServiceId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, NULL, N'98uW2t1Lx2sChqQrstV/Z+btgyblzYKKf7NzfzboqelRE34uxK6Xsw3hSa8w9Ppk', N'WN1t08WpFmMMRhbYz6Af2T7Ne+4iRDPXRN3ni20L0CklCc3Ij3NEy1twH0ta1dMP', NULL, 1, 3, 5, 3, @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=')
    INSERT [dbo].[Referrals] ([Id], [ReferrerTelephone], [ReasonForSupport], [EngageWithFamily], [ReasonForDecliningSupport], [StatusId], [RecipientId], [UserAccountId], [ReferralServiceId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, NULL, N'5SYqYQwAUhjBb07oTPB/2rBeYyitRsBiz+t+ai78yDU=', N'gNgnLSYJxrrl4z58fNfdWMLFc0PLhx5nE+W1EN7ryVw=', NULL, 1, 4, 5, 1, @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=')
    
    SET IDENTITY_INSERT [dbo].[Referrals] OFF
    
    DELETE FROM [dbo].[Roles]

    INSERT [dbo].[Roles] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, N'DfeAdmin', N'DfE Administrator', @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Roles] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, N'LaManager', N'Local Authority Manager', @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Roles] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, N'VcsManager', N'VCS Manager', @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Roles] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, N'LaProfessional', N'Local Authority Professional', @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Roles] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, N'VcsProfessional', N'VCS Professional', @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Roles] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (6, N'LaDualRole', N'Local Authority Dual Role', @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    INSERT [dbo].[Roles] ([Id], [Name], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, N'VcsDualRole', N'VCS Dual Role', @Today, N'wSwXVaACiNZ54lxiEnuMLw==', @Today, N'wSwXVaACiNZ54lxiEnuMLw==')
    
    DELETE FROM [dbo].[UserAccountRoles]
    
    SET IDENTITY_INSERT [dbo].[UserAccountRoles] ON

    INSERT [dbo].[UserAccountRoles] ([Id], [UserAccountId], [RoleId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, 8, 6, @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=')
    INSERT [dbo].[UserAccountRoles] ([Id], [UserAccountId], [RoleId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, 5, 6, @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=')
    
    SET IDENTITY_INSERT [dbo].[UserAccountRoles] OFF
    
    DELETE FROM [dbo].[ConnectionRequestsSentMetric]
    
    SET IDENTITY_INSERT [dbo].[ConnectionRequestsSentMetric] ON

    INSERT [dbo].[ConnectionRequestsSentMetric] ([Id], [LaOrganisationId], [UserAccountId], [RequestTimestamp], [RequestCorrelationId], [ResponseTimestamp], [HttpResponseCode], [ConnectionRequestId], [ConnectionRequestReferenceCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [VcsOrganisationId]) VALUES (1, 6, 8, @Today, N'f24e16e5cd60cfb52c2efe15a6f878c7', @Today, 200, 1, N'000001', @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', @Today, N'EInniC1L0UOibN+KFhtTe2qu+0oCinIQLj87AWnhbfU=', 11)
    INSERT [dbo].[ConnectionRequestsSentMetric] ([Id], [LaOrganisationId], [UserAccountId], [RequestTimestamp], [RequestCorrelationId], [ResponseTimestamp], [HttpResponseCode], [ConnectionRequestId], [ConnectionRequestReferenceCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [VcsOrganisationId]) VALUES (2, 6, 5, @Today, N'd72fb29dccb170cf23dd71b8c7d41d5a', @Today, 200, 2, N'000002', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', 11)
    INSERT [dbo].[ConnectionRequestsSentMetric] ([Id], [LaOrganisationId], [UserAccountId], [RequestTimestamp], [RequestCorrelationId], [ResponseTimestamp], [HttpResponseCode], [ConnectionRequestId], [ConnectionRequestReferenceCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [VcsOrganisationId]) VALUES (3, 6, 5, @Today, N'46632d5024c4ee355efdfe804755dc36', @Today, 200, 3, N'000003', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', 11)
    INSERT [dbo].[ConnectionRequestsSentMetric] ([Id], [LaOrganisationId], [UserAccountId], [RequestTimestamp], [RequestCorrelationId], [ResponseTimestamp], [HttpResponseCode], [ConnectionRequestId], [ConnectionRequestReferenceCode], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [VcsOrganisationId]) VALUES (4, 6, 5, @Today, N'20356e51c206c6fb5a545e93d1501ca9', @Today, 200, 4, N'000004', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', @Today, N'E49KDBz/ow+ifh63D63sUr5iBLZMnAeKlAXVitdSveo=', 11)

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
