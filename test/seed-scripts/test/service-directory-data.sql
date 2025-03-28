BEGIN TRY
    BEGIN TRANSACTION
        
    DECLARE @Today DATETIME2
    SET @Today = GETDATE()

    -- 1st week
    DECLARE @StartOfFirstWeekOf4 DATETIME2
    SET @StartOfFirstWeekOf4 = DATEADD(day, -(DATEPART(WEEKDAY, @Today) + 6), CONVERT(DATE, @Today))
    
    -- 2nd week - ignore the 1 second off midnight and use the previous week end
    DECLARE @StartOfSecondWeekOf4 DATETIME2
    SET @StartOfSecondWeekOf4 = DATEADD(day, -(DATEPART(WEEKDAY, @StartOfFirstWeekOf4) + 6), CONVERT(DATE, @StartOfFirstWeekOf4))
    
    -- 3rd week - ignore the 1 second off midnight and use the previous week end
    DECLARE @StartOfThirdWeekOf4 DATETIME2
    SET @StartOfThirdWeekOf4 = DATEADD(day, -(DATEPART(WEEKDAY, @StartOfSecondWeekOf4) + 6), CONVERT(DATE, @StartOfSecondWeekOf4))
    
    -- 4th week - ignore the 1 second off midnight and use the previous week end
    DECLARE @StartOfForthWeekOf4 DATETIME2
    SET @StartOfForthWeekOf4 = DATEADD(day, -(DATEPART(WEEKDAY, @StartOfThirdWeekOf4) + 6), CONVERT(DATE, @StartOfThirdWeekOf4))

    DELETE FROM [dbo].[ServiceAtLocations]
    DELETE FROM [dbo].[Locations]
    DELETE FROM [dbo].[Organisations]
    DELETE FROM [dbo].[ServiceSearchResults]
    DELETE FROM [dbo].[Services]
    DELETE FROM [dbo].[Schedules]
    DELETE FROM [dbo].[Contacts]
    DELETE FROM [dbo].[Taxonomies]
    DELETE FROM [dbo].[ServiceTaxonomies]
    DELETE FROM [dbo].[Eligibilities]
    DELETE FROM [dbo].[Languages]
    DELETE FROM [dbo].[ServiceDeliveries]
    DELETE FROM [dbo].[ServiceSearches]
    DELETE FROM [dbo].[Events]
    DELETE FROM [dbo].[ServiceTypes]


    SET IDENTITY_INSERT [dbo].[Organisations] ON

    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, N'LA', N'Bristol County Council', N'Bristol County Council', N'E06000023', NULL, NULL, N'https://www.bristol.gov.uk/', N'https://www.bristol.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, N'LA', N'Lancashire County Council', N'Lancashire County Council', N'E10000017', NULL, NULL, N'https://www.lancashire.gov.uk/', N'https://www.lancashire.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, N'LA', N'London Borough of Redbridge', N'London Borough of Redbridge', N'E09000026', NULL, NULL, N'https://www.redbridge.gov.uk/', N'https://www.redbridge.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, N'LA', N'Salford City Council', N'Salford City Council', N'E08000006', NULL, NULL, N'https://www.salford.gov.uk/', N'https://www.salford.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, N'LA', N'Suffolk County Council', N'Suffolk County Council', N'E10000029', NULL, NULL, N'https://www.suffolk.gov.uk/', N'https://www.suffolk.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (6, N'LA', N'Tower Hamlets Council', N'Tower Hamlets Council', N'E09000030', NULL, NULL, N'https://www.towerhamlets.gov.uk/', N'https://www.towerhamlets.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, N'LA', N'Lewisham Council', N'Lewisham Council', N'E09000023', NULL, NULL, N'https://lewisham.gov.uk/', N'https://lewisham.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, N'LA', N'North East Lincolnshire Council', N'North East Lincolnshire Council', N'E06000012', NULL, NULL, N'https://www.nelincs.gov.uk/', N'https://www.nelincs.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (9, N'LA', N'City of Wolverhampton Council', N'City of Wolverhampton Council', N'E08000031', NULL, NULL, N'https://www.wolverhampton.gov.uk/', N'https://www.wolverhampton.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, N'LA', N'Sheffield City Council', N'Sheffield City Council', N'E08000019', NULL, NULL, N'https://www.sheffield.gov.uk/', N'https://www.sheffield.gov.uk/', @Today, NULL, @Today, NULL)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, N'VCFS', N'Elop Mentoring', N'Elop Mentoring', N'E09000030', 6, NULL, NULL, NULL, @Today, 1, @Today, 1)
    INSERT [dbo].[Organisations] ([Id], [OrganisationType], [Name], [Description], [AdminAreaCode], [AssociatedOrganisationId], [Logo], [Uri], [Url], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, N'VCFS', N'Bristol Local Offer', N'Bristol Local Offer', N'E06000023', 1, NULL, NULL, NULL, @Today, 4, @Today, 4)

    SET IDENTITY_INSERT [dbo].[Organisations] OFF


    SET IDENTITY_INSERT [dbo].[Services] ON

    INSERT [dbo].[Services] ([Id], [ServiceType], [Name], [Description], [Status], [Fees], [Accreditations], [DeliverableType], [AssuredDate], [CanFamilyChooseDeliveryLocation], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [OrganisationId], [InterpretationServices], [Summary]) VALUES (1, N'InformationSharing', N'LGBT+ Asylum support', NULL, N'Active', NULL, NULL, N'NotSet', NULL, 0, @Today, 1, @Today, 1, 11, N'translation,bsl', N'elop offer a range of LGBT+ specialist mental health support, advice, information, counselling, and well being support with a wide range of community connection services for LGBT+ communities.')
    INSERT [dbo].[Services] ([Id], [ServiceType], [Name], [Description], [Status], [Fees], [Accreditations], [DeliverableType], [AssuredDate], [CanFamilyChooseDeliveryLocation], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [OrganisationId], [InterpretationServices], [Summary]) VALUES (2, N'FamilyExperience', N'Queen Mary Legal Advice Centre ', NULL, N'Active', NULL, NULL, N'NotSet', NULL, 0, @Today, 1, @Today, 1, 6, N'translation,bsl', N'Queen Mary Legal Advice Centre (QMLAC) offers free legal advice in a range of different legal areas.')
    INSERT [dbo].[Services] ([Id], [ServiceType], [Name], [Description], [Status], [Fees], [Accreditations], [DeliverableType], [AssuredDate], [CanFamilyChooseDeliveryLocation], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [OrganisationId], [InterpretationServices], [Summary]) VALUES (3, N'InformationSharing', N'Young People’s Services ‘Youth Out East’', N'In addition to operating direct community frontline services ELOP also delivers second-tier work which includes providing information, training, consultancy and support to statutory and voluntary sector policy makers, managers, service providers and their staff teams.', N'Active', NULL, NULL, N'NotSet', NULL, 0, @Today, 3, @Today, 3, 11, N'translation,bsl', N'ELOP has been providing over 14 years of information, advice, advocacy, counselling and support services, plus other social and community activities and events to northeast London’s LGBT communities. ')
    INSERT [dbo].[Services] ([Id], [ServiceType], [Name], [Description], [Status], [Fees], [Accreditations], [DeliverableType], [AssuredDate], [CanFamilyChooseDeliveryLocation], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [OrganisationId], [InterpretationServices], [Summary]) VALUES (4, N'InformationSharing', N'Bristol Autism Team (BAT)', N'The BAT offers mainstream education settings:
    a named BAT link from early years through to post 16 (sixth form and college)
    annual cohort meetings with their named BAT link to plan autism support for their setting 
    annual advice clinics advice for parents and carers through our online referral system, which parents and carers can access by emailing bristol.autism.hotline@bristol.gov.uk.', N'Active', NULL, NULL, N'NotSet', NULL, 0, @Today, 4, @Today, 4, 12, N'translation,bsl', N'The Bristol Autism Team (BAT) supports children and young people from the age of 4 to 18.')
    INSERT [dbo].[Services] ([Id], [ServiceType], [Name], [Description], [Status], [Fees], [Accreditations], [DeliverableType], [AssuredDate], [CanFamilyChooseDeliveryLocation], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [OrganisationId], [InterpretationServices], [Summary]) VALUES (5, N'FamilyExperience', N'Elop LGBT+ Support', NULL, N'Active', NULL, NULL, N'NotSet', NULL, 0, @Today, 4, @Today, 4, 6, N'', N'We are an exciting innovative lesbian and gay mental health charity based in East London. ELOP was established around 1995 as a grassroots developed and community-led organisation.')

    SET IDENTITY_INSERT [dbo].[Services] OFF
    
    
    SET IDENTITY_INSERT [dbo].[Locations] ON
    
    INSERT [dbo].[Locations] ([Id], [LocationTypeCategory], [Name], [Description], [Latitude], [Longitude], [Address1], [Address2], [City], [PostCode], [StateProvince], [Country], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [AddressType], [AlternateName], [Attention], [ExternalIdentifier], [ExternalIdentifierType], [LocationType], [Region], [Transportation], [Url], [OrganisationId], [GeoPoint]) VALUES (1, N'NotSet', N'Elop Community Centre', N'020 8509 3898
        
        Saturday	Closed
        Sunday	Closed
        Monday	9 am–9:30 pm
        Tuesday	9 am–9:30 pm
        Wednesday	9 am–9:30 pm
        Thursday	9 am–9:30 pm
        Friday	9 am–4:30 pm
        ', 51.578693389892578, -0.014600999653339386, N'56-60 Grove Rd', NULL, N'London', N'E17 9BN', N'', N'GB', @Today, 1, @Today, 1, NULL, NULL, NULL, NULL, NULL, N'Postal', NULL, NULL, NULL, NULL, 0xE6100000010C000000A012CA49400000000021E78DBF)
    INSERT [dbo].[Locations] ([Id], [LocationTypeCategory], [Name], [Description], [Latitude], [Longitude], [Address1], [Address2], [City], [PostCode], [StateProvince], [Country], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [AddressType], [AlternateName], [Attention], [ExternalIdentifier], [ExternalIdentifierType], [LocationType], [Region], [Transportation], [Url], [OrganisationId], [GeoPoint]) VALUES (2, N'NotSet', N'Queen Mary University of London ', NULL, 51.524635314941406, -0.040709998458623886, N'Mile End Road ', NULL, N'London ', N'E1 4NS', N'', N'GB', @Today, 1, @Today, 1, NULL, NULL, NULL, NULL, NULL, N'Postal', NULL, NULL, NULL, NULL, 0xE6100000010C0000004027C34940000000E0F0D7A4BF)
    INSERT [dbo].[Locations] ([Id], [LocationTypeCategory], [Name], [Description], [Latitude], [Longitude], [Address1], [Address2], [City], [PostCode], [StateProvince], [Country], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [AddressType], [AlternateName], [Attention], [ExternalIdentifier], [ExternalIdentifierType], [LocationType], [Region], [Transportation], [Url], [OrganisationId], [GeoPoint]) VALUES (3, N'NotSet', N'ELOP – East London Out Project Registered Office ', N'A comfy, easy-going hang-out space to meet up, eat up and relax with friends, plus a great way to meet new faces right on your doorstep.', 51.578693389892578, -0.014600999653339386, N'56-60 Grove Road', N'Walthamstow', N'London', N'E17 9BN', N'', N'GB', @Today, 3, @Today, 3, NULL, NULL, NULL, NULL, NULL, N'Postal', NULL, NULL, NULL, 11, 0xE6100000010C000000A012CA49400000000021E78DBF)
    INSERT [dbo].[Locations] ([Id], [LocationTypeCategory], [Name], [Description], [Latitude], [Longitude], [Address1], [Address2], [City], [PostCode], [StateProvince], [Country], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [AddressType], [AlternateName], [Attention], [ExternalIdentifier], [ExternalIdentifierType], [LocationType], [Region], [Transportation], [Url], [OrganisationId], [GeoPoint]) VALUES (4, N'NotSet', N'City Hall', NULL, 51.458454132080078, -2.5741660594940186, N'Trading with Schools', N'PO Box 3399', N'Bristol', N'BS1 9NE', N'', N'GB', @Today, 4, @Today, 4, NULL, NULL, NULL, NULL, NULL, N'Postal', NULL, NULL, NULL, NULL, 0xE6100000010C000000A0AEBA494000000060E49704C0)
    INSERT [dbo].[Locations] ([Id], [LocationTypeCategory], [Name], [Description], [Latitude], [Longitude], [Address1], [Address2], [City], [PostCode], [StateProvince], [Country], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [AddressType], [AlternateName], [Attention], [ExternalIdentifier], [ExternalIdentifierType], [LocationType], [Region], [Transportation], [Url], [OrganisationId], [GeoPoint]) VALUES (5, N'FamilyHub', N' John Smith Family Hub', N'Mon-Fri 9.15am-5pm', 51.517612457275391, -0.056837998330593109, N'90 Stepney Way', NULL, N'London', N'E1 2EN', N'', N'GB', @Today, 4, @Today, 4, NULL, NULL, NULL, NULL, NULL, N'Postal', NULL, NULL, NULL, NULL, 0xE6100000010C0000002041C24940000000C0DE19ADBF)
    INSERT [dbo].[Locations] ([Id], [LocationTypeCategory], [Name], [Description], [Latitude], [Longitude], [Address1], [Address2], [City], [PostCode], [StateProvince], [Country], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [AddressType], [AlternateName], [Attention], [ExternalIdentifier], [ExternalIdentifierType], [LocationType], [Region], [Transportation], [Url], [OrganisationId], [GeoPoint]) VALUES (6, N'FamilyHub', N'Ocean Children and Family Centre - Whitehorse', N'Mon-Fri 9.15am-5pm', 51.513690948486328, -0.040619999170303345, N'White Horse Rd', NULL, N'London', N'E1 0ND', N'', N'GB', @Today, 5, @Today, 5, NULL, NULL, NULL, NULL, NULL, N'Postal', NULL, NULL, NULL, 6, 0xE6100000010C000000A0C0C149400000000025CCA4BF)
    INSERT [dbo].[Locations] ([Id], [LocationTypeCategory], [Name], [Description], [Latitude], [Longitude], [Address1], [Address2], [City], [PostCode], [StateProvince], [Country], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [AddressType], [AlternateName], [Attention], [ExternalIdentifier], [ExternalIdentifierType], [LocationType], [Region], [Transportation], [Url], [OrganisationId], [GeoPoint]) VALUES (7, N'FamilyHub', N'Wapping And Bigland Children And Family Centre', N'Mon-Fri 9am-5pm', 51.514507293701172, -0.060430999845266342, N'15 Richard St', NULL, N'London', N'E1 2JP', N'', N'GB', @Today, 4, @Today, 4, NULL, NULL, NULL, NULL, NULL, N'Postal', NULL, NULL, NULL, NULL, 0xE6100000010C00000060DBC14940000000E0CFF0AEBF)
    
    SET IDENTITY_INSERT [dbo].[Locations] OFF

    
    SET IDENTITY_INSERT [dbo].[ServiceAtLocations] ON

    INSERT [dbo].[ServiceAtLocations] ([ServiceId], [LocationId], [Id], [Created], [CreatedBy], [Description], [LastModified], [LastModifiedBy]) VALUES (1, 1, 1, @Today, 1, NULL, @Today, 1)
    INSERT [dbo].[ServiceAtLocations] ([ServiceId], [LocationId], [Id], [Created], [CreatedBy], [Description], [LastModified], [LastModifiedBy]) VALUES (2, 2, 2, @Today, 1, NULL, @Today, 1)
    INSERT [dbo].[ServiceAtLocations] ([ServiceId], [LocationId], [Id], [Created], [CreatedBy], [Description], [LastModified], [LastModifiedBy]) VALUES (3, 3, 3, @Today, 3, NULL, @Today, 3)
    INSERT [dbo].[ServiceAtLocations] ([ServiceId], [LocationId], [Id], [Created], [CreatedBy], [Description], [LastModified], [LastModifiedBy]) VALUES (4, 4, 4, @Today, 4, NULL, @Today, 4)
    INSERT [dbo].[ServiceAtLocations] ([ServiceId], [LocationId], [Id], [Created], [CreatedBy], [Description], [LastModified], [LastModifiedBy]) VALUES (5, 7, 5, @Today, 4, NULL, @Today, 4)

    SET IDENTITY_INSERT [dbo].[ServiceAtLocations] OFF
    

    SET IDENTITY_INSERT [dbo].[Schedules] ON

    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (1, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TH,SA', NULL, NULL, @Today, 1, @Today, 1, 1, NULL, NULL, N'Online', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (2, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TH,SA', NULL, NULL, @Today, 1, @Today, 1, 1, NULL, NULL, N'Telephone', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (3, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TH', NULL, N'4.30-5.30pm', @Today, 1, @Today, 1, NULL, NULL, NULL, N'InPerson', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (4, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TH,SA', NULL, N'Please visit the website for a list of dates.', @Today, 1, @Today, 1, 2, NULL, NULL, N'Online', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (5, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TH,SA', NULL, N'Please visit the website for a list of dates.', @Today, 1, @Today, 1, 2, NULL, NULL, N'Telephone', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (6, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TU,TH', NULL, N'We have bi-weekly drop in clinics on a Monday evening between 5.30–7.30pm for help filling out your welfare benefits applications or undertaking a mandatory reconsideration appeal.', @Today, 1, @Today, 1, NULL, NULL, NULL, N'InPerson', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (7, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TU,FR', NULL, NULL, @Today, 3, @Today, 3, 3, NULL, NULL, N'Online', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (8, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TU,FR', NULL, NULL, @Today, 3, @Today, 3, 3, NULL, NULL, N'Telephone', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (9, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TU,WE', NULL, N'Closed 26th February.', @Today, 3, @Today, 3, NULL, NULL, NULL, N'InPerson', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (13, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'MO,TU,WE,TH,FR', NULL, N'We support these children and young people through:
        
        primary school
        secondary school
        post 16 education settings', @Today, 4, @Today, 4, 4, NULL, NULL, N'Online', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (14, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'MO,TU,WE,TH,FR', NULL, N'We support these children and young people through:
        
        primary school
        secondary school
        post 16 education settings', @Today, 4, @Today, 4, 4, NULL, NULL, N'Telephone', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (15, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'WE,TH,FR', NULL, NULL, @Today, 4, @Today, 4, NULL, NULL, NULL, N'InPerson', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4)
    INSERT [dbo].[Schedules] ([Id], [OpensAt], [ClosesAt], [ValidFrom], [ValidTo], [DtStart], [Freq], [Interval], [ByDay], [ByMonthDay], [Description], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId], [Timezone], [AttendingType], [ByWeekNo], [ByYearDay], [Count], [Notes], [ScheduleLink], [Until], [WkSt], [ServiceAtLocationId]) VALUES (16, NULL, NULL, NULL, NULL, NULL, N'WEEKLY', NULL, N'TH,FR', NULL, NULL, @Today, 4, @Today, 4, NULL, NULL, NULL, N'InPerson', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5)
    SET IDENTITY_INSERT [dbo].[Schedules] OFF
    
    
    SET IDENTITY_INSERT [dbo].[Contacts] ON 
    
    INSERT [dbo].[Contacts] ([Id], [Title], [Name], [Telephone], [TextPhone], [Url], [Email], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId]) VALUES (1, NULL, NULL, N'020 8509 3898 ', N'020 8509 3898 ', NULL, N'info@elop.org', @Today, 1, @Today, 1, 1, NULL)
    INSERT [dbo].[Contacts] ([Id], [Title], [Name], [Telephone], [TextPhone], [Url], [Email], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId]) VALUES (2, NULL, NULL, N'+44 (0) 20 7882 5555', NULL, N'www.qmul.ac.uk/lac/', N'lac@qmul.ac.uk', @Today, 1, @Today, 1, 2, NULL)
    INSERT [dbo].[Contacts] ([Id], [Title], [Name], [Telephone], [TextPhone], [Url], [Email], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId]) VALUES (3, NULL, NULL, N'020 8509 3898  ', NULL, NULL, N'info@elop.org', @Today, 3, @Today, 3, 3, NULL)
    INSERT [dbo].[Contacts] ([Id], [Title], [Name], [Telephone], [TextPhone], [Url], [Email], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId]) VALUES (5, NULL, NULL, N'', NULL, N'tradingwithschools.org/Page/4784', N'bristol.autism.hotline@bristol.gov.uk', @Today, 4, @Today, 4, 4, NULL)
    INSERT [dbo].[Contacts] ([Id], [Title], [Name], [Telephone], [TextPhone], [Url], [Email], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [LocationId]) VALUES (6, NULL, NULL, N'', NULL, N'www.elop.org/', NULL, @Today, 4, @Today, 4, 5, NULL)
    
    SET IDENTITY_INSERT [dbo].[Contacts] OFF
    
    SET IDENTITY_INSERT [dbo].[Taxonomies] ON 
    
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, N'Activities, clubs and groups', N'ServiceCategory', NULL, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, N'Family support', N'ServiceCategory', NULL, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, N'Health', N'ServiceCategory', NULL, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, N'Pregnancy, birth and early years', N'ServiceCategory', NULL, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, N'Special educational needs and disabilities (SEND)', N'ServiceCategory', NULL, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (6, N'Transport', N'ServiceCategory', NULL, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, N'Activities', N'ServiceCategory', 1, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, N'Before and after school clubs', N'ServiceCategory', 1, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (9, N'Holiday clubs and schemes', N'ServiceCategory', 1, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, N'Music, arts and dance', N'ServiceCategory', 1, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, N'Parent, baby and toddler groups', N'ServiceCategory', 1, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, N'Pre-school playgroup', N'ServiceCategory', 1, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (13, N'Sports and recreation', N'ServiceCategory', 1, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (14, N'Bullying and cyber bullying', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (15, N'Debt and welfare advice', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (16, N'Domestic abuse', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (17, N'Intensive targeted family support', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (18, N'Money, benefits and housing', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (19, N'Parenting support', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (20, N'Reducing parental conflict', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (21, N'Separating and separated parent support', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (22, N'Stopping smoking', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (23, N'Substance misuse (including alcohol and drug)', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (24, N'Targeted youth support', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (25, N'Youth justice services', N'ServiceCategory', 2, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (26, N'Hearing and sight', N'ServiceCategory', 3, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (27, N'Mental health, social and emotional support', N'ServiceCategory', 3, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (28, N'Nutrition and weight management', N'ServiceCategory', 3, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (29, N'Oral health', N'ServiceCategory', 3, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (30, N'Public health services', N'ServiceCategory', 3, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (31, N'Birth registration', N'ServiceCategory', 4, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (32, N'Early years language and learning', N'ServiceCategory', 4, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (33, N'Health visiting', N'ServiceCategory', 4, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (34, N'Infant feeding support (including breastfeeding)', N'ServiceCategory', 4, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (35, N'Midwife and maternity', N'ServiceCategory', 4, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (36, N'Perinatal mental health support (pregnancy to one year post birth)', N'ServiceCategory', 4, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (37, N'Autistic Spectrum Disorder (ASD)', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (38, N'Breaks and respite', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (39, N'Early years support', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (40, N'Groups for parents and carers of children with SEND', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (41, N'Hearing impairment', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (42, N'Learning difficulties and disabilities', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (43, N'Multi-sensory impairment', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (44, N'Other difficulties or disabilities', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (45, N'Physical disabilities', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (46, N'Social, emotional and mental health support', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (47, N'Speech, language and communication needs', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (48, N'Visual impairment', N'ServiceCategory', 5, @Today, NULL, @Today, NULL)
    INSERT [dbo].[Taxonomies] ([Id], [Name], [TaxonomyType], [ParentId], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (49, N'Community transport', N'ServiceCategory', 6, @Today, NULL, @Today, NULL)
    
    SET IDENTITY_INSERT [dbo].[Taxonomies] OFF

    
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (5, 15)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (1, 18)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (2, 24)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (3, 24)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (2, 25)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (3, 25)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 37)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 38)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 39)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 40)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 41)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 42)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 43)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 44)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 45)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 46)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 47)
    INSERT [dbo].[ServiceTaxonomies] ([ServiceId], [TaxonomyId]) VALUES (4, 48)

    
    SET IDENTITY_INSERT [dbo].[Eligibilities] ON 
    
    INSERT [dbo].[Eligibilities] ([Id], [EligibilityType], [MaximumAge], [MinimumAge], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (1, NULL, 127, 0, @Today, 3, @Today, 3, 3)
    INSERT [dbo].[Eligibilities] ([Id], [EligibilityType], [MaximumAge], [MinimumAge], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (3, NULL, 18, 4, @Today, 4, @Today, 4, 4)
    INSERT [dbo].[Eligibilities] ([Id], [EligibilityType], [MaximumAge], [MinimumAge], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (4, NULL, 25, 4, @Today, 4, @Today, 4, 5)
    
    SET IDENTITY_INSERT [dbo].[Eligibilities] OFF

    
    SET IDENTITY_INSERT [dbo].[Languages] ON 
    
    INSERT [dbo].[Languages] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [Code], [Note]) VALUES (1, N'English', @Today, 1, @Today, 1, 1, N'en', NULL)
    INSERT [dbo].[Languages] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [Code], [Note]) VALUES (2, N'English', @Today, 1, @Today, 1, 2, N'en', NULL)
    INSERT [dbo].[Languages] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [Code], [Note]) VALUES (3, N'English', @Today, 3, @Today, 3, 3, N'en', NULL)
    INSERT [dbo].[Languages] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [Code], [Note]) VALUES (4, N'Danish', @Today, 3, @Today, 3, 3, N'da', NULL)
    INSERT [dbo].[Languages] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [Code], [Note]) VALUES (6, N'English', @Today, 4, @Today, 4, 4, N'en', NULL)
    INSERT [dbo].[Languages] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId], [Code], [Note]) VALUES (7, N'English', @Today, 4, @Today, 4, 5, N'en', NULL)
    
    SET IDENTITY_INSERT [dbo].[Languages] OFF

    
    SET IDENTITY_INSERT [dbo].[ServiceDeliveries] ON 
    
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (1, N'InPerson', @Today, 1, @Today, 1, 1)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (2, N'Online', @Today, 1, @Today, 1, 1)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (3, N'Telephone', @Today, 1, @Today, 1, 1)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (4, N'InPerson', @Today, 1, @Today, 1, 2)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (5, N'Online', @Today, 1, @Today, 1, 2)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (6, N'Telephone', @Today, 1, @Today, 1, 2)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (7, N'InPerson', @Today, 3, @Today, 3, 3)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (8, N'Online', @Today, 3, @Today, 3, 3)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (9, N'Telephone', @Today, 3, @Today, 3, 3)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (13, N'InPerson', @Today, 4, @Today, 4, 4)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (14, N'Online', @Today, 4, @Today, 4, 4)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (15, N'Telephone', @Today, 4, @Today, 4, 4)
    INSERT [dbo].[ServiceDeliveries] ([Id], [Name], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ServiceId]) VALUES (16, N'InPerson', @Today, 4, @Today, 4, 5)
    
    SET IDENTITY_INSERT [dbo].[ServiceDeliveries] OFF
    
    INSERT [dbo].[Events] ([Id], [Name], [Description]) VALUES (1, N'ServiceDirectoryInitialSearch', N'Describes an initial, unfiltered search by a user.')
    INSERT [dbo].[Events] ([Id], [Name], [Description]) VALUES (2, N'ServiceDirectorySearchFilter', N'Describes a filtered search by a user.')
    
    
    INSERT [dbo].[ServiceTypes] ([Id], [Name], [Description]) VALUES (1, N'InformationSharing', N'Connect')
    INSERT [dbo].[ServiceTypes] ([Id], [Name], [Description]) VALUES (2, N'FamilyExperience', N'Find')

    
    SET IDENTITY_INSERT [dbo].[ServiceSearches] ON 
    
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (1, 1, N'E1 5LX', 20, NULL, 200, @Today, @Today, N'9fec60cc-0c45-4419-9c89-85042636c65f', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (2, 1, N'E1 2EN', 20, NULL, 200, @Today, @Today, N'0d7f8f1c-1735-4679-b391-ac6d8f0fefcc', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (3, 1, N'E1 2EN', 20, NULL, 200, @Today, @Today, N'5afbc145-3235-4920-bd06-507035e380ba', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (4, 2, N'E1 2EN', 20, NULL, 200, @Today, @Today, N'5afbc145-3235-4920-bd06-507035e380ba', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (5, 1, N'E1 2EN', 20, NULL, 200, @Today, @Today, N'39796c61-27c4-4ee6-852a-c43e8af27ca7', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (6, 1, N'E1 2EN', 20, NULL, 200, @Today, @Today, N'38ca96cf-34c4-48ba-afdf-ff154964138f', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (7, 1, N'E1 2EN', 20, NULL, 200, @Today, @Today, N'590325b4-b3de-4299-9a28-7c10093c2435', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (8, 1, N'E1 2EN', 0, 5, 200, @StartOfFirstWeekOf4, @StartOfFirstWeekOf4, N'809f1556-b482-4227-93ee-4cd2557870cf', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (9, 1, N'E1 2EN', 0, 5, 200, @StartOfFirstWeekOf4, @StartOfFirstWeekOf4, N'b42638a7-e45f-4989-b53c-6a165ebcb564', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (10, 1, N'E1 2EN', 0, 5, 200, @StartOfFirstWeekOf4, @StartOfFirstWeekOf4, N'a9e2a502-b81b-46c8-8c15-d72c70724e69', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (11, 1, N'E1 2EN', 0, 5, 200, @StartOfFirstWeekOf4, @StartOfFirstWeekOf4, N'1538dc52-4f68-4c59-97af-f94647ffc6fe', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (12, 1, N'E1 2EN', 0, 5, 200, @StartOfSecondWeekOf4, @StartOfSecondWeekOf4, N'4ecf0b7d-56ba-475f-a723-85a9f0a7ae91', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (13, 1, N'E1 2EN', 0, 3, 200, @StartOfSecondWeekOf4, @StartOfSecondWeekOf4, N'72f9ed0e-b925-4901-9ccc-fce588ec657b', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (14, 1, N'E1 2EN', 0, 3, 200, @StartOfSecondWeekOf4, @StartOfSecondWeekOf4, N'a6ed8a8c-2420-44d8-bde8-bb10f2d20149', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (15, 1, N'E1 2EN', 0, 3, 200, @StartOfSecondWeekOf4, @StartOfSecondWeekOf4, N'fd864ddc-af9f-4551-bc7e-6724ab41f352', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (16, 1, N'E1 2EN', 0, 3, 200, @StartOfSecondWeekOf4, @StartOfSecondWeekOf4, N'e2451856-850d-49d5-800f-8fac63a04727', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (17, 1, N'E1 2EN', 0, 3, 200, @StartOfSecondWeekOf4, @StartOfSecondWeekOf4, N'c391f06b-c3ef-4604-a124-fb8a3a7d351a', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (18, 1, N'E1 2EN', 0, 3, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'509b016b-9047-4be3-8b15-5fd3306476b3', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (19, 1, N'E1 2EN', 0, 3, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'63c1db65-6c85-48d6-97e8-84297019e5e8', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (20, 1, N'E1 2EN', 20, NULL, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'4e31206e-d830-4118-a7ec-86f38b30cf82', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (21, 1, N'e1 2en', 0, 9, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'b098dc3a-d6fe-4fe2-bd17-46a6a140f443', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (22, 1, N'e1 2en', 0, 8, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'c3b8c5b8-c843-4724-97be-adaff6924d47', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (23, 1, N'e1 2en', 0, 9, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'99f2705b-cc1e-4f36-bdfd-6b46cb50228d', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (24, 1, N'e1 2en', 0, 5, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'c1f7efe6-640d-4b0d-956f-1bbfd9f987a0', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (25, 1, N'e1 2en', 0, 8, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'46143258-506c-4d96-9a9e-cc4ec140f31b', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (26, 1, N'e1 2en', 0, 9, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'49323831-3adc-4ee6-850d-03c2d137cdc9', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (27, 1, N'e1 2en', 0, 9, 200, @StartOfThirdWeekOf4, @StartOfThirdWeekOf4, N'8ff0b748-1f7f-4c47-8322-cb96953e5f05', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (28, 1, N'e1 2en', 0, 5, 200, @StartOfForthWeekOf4, @StartOfForthWeekOf4, N'137145cd-98c8-469d-825b-f8b74cc7b0a9', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (29, 1, N'e1 2en', 0, 5, 200, @StartOfForthWeekOf4, @StartOfForthWeekOf4, N'1e9ac7f1-021e-440f-810b-f3205f9a975b', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (30, 1, N'bs2 0sp', 0, 5, 200, @StartOfForthWeekOf4, @StartOfForthWeekOf4, N'664dd8ab-7376-48d1-b813-747621850ad5', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (31, 1, N'e1 2en', 0, 5, 200, @StartOfForthWeekOf4, @StartOfForthWeekOf4, N'1a291b27-70b5-4173-b55d-6004762d2efc', 1, NULL)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (32, 1, N'E1 2EN', 20, NULL, 200, @StartOfForthWeekOf4, @StartOfForthWeekOf4, N'815127b8-b85d-4643-b9c2-d3c96897739a', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (33, 2, N'E1 2EN', 20, NULL, 200, @StartOfForthWeekOf4, @StartOfForthWeekOf4, N'815127b8-b85d-4643-b9c2-d3c96897739a', 2, 6)
    INSERT [dbo].[ServiceSearches] ([Id], [SearchTriggerEventId], [SearchPostcode], [SearchRadiusMiles], [UserId], [HttpResponseCode], [RequestTimestamp], [ResponseTimestamp], [CorrelationId], [ServiceSearchTypeId], [OrganisationId]) VALUES (34, 2, N'E1 2EN', 20, NULL, 200, @StartOfForthWeekOf4, @StartOfForthWeekOf4, N'815127b8-b85d-4643-b9c2-d3c96897739a', 2, 6)
    
    SET IDENTITY_INSERT [dbo].[ServiceSearches] OFF

    
    SET IDENTITY_INSERT [dbo].[ServiceSearchResults] ON 
    
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (1, 2, 3)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (2, 2, 4)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (3, 2, 5)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (4, 2, 6)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (5, 2, 7)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (6, 1, 8)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (7, 3, 8)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (8, 1, 9)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (9, 3, 9)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (10, 1, 10)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (11, 3, 10)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (12, 1, 11)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (13, 3, 11)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (14, 1, 12)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (15, 3, 12)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (16, 1, 13)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (17, 3, 13)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (18, 1, 14)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (19, 3, 14)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (20, 1, 15)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (21, 3, 15)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (22, 1, 16)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (23, 3, 16)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (24, 1, 17)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (25, 3, 17)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (26, 1, 18)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (27, 3, 18)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (28, 1, 19)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (29, 3, 19)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (30, 5, 20)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (31, 2, 20)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (32, 1, 21)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (33, 3, 21)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (34, 1, 22)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (35, 3, 22)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (36, 1, 23)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (37, 3, 23)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (38, 1, 24)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (39, 3, 24)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (40, 1, 25)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (41, 3, 25)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (42, 1, 26)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (43, 3, 26)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (44, 1, 27)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (45, 3, 27)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (46, 1, 28)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (47, 3, 28)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (48, 1, 29)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (49, 3, 29)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (50, 4, 30)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (51, 1, 31)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (52, 3, 31)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (53, 5, 32)
    INSERT [dbo].[ServiceSearchResults] ([Id], [ServiceId], [ServiceSearchId]) VALUES (54, 2, 32)
    
    SET IDENTITY_INSERT [dbo].[ServiceSearchResults] OFF
    
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