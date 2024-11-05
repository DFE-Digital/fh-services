SELECT [Id]
        ,[OpenId]
        ,[Name]
        ,[Email]
        ,[PhoneNumber]
        ,[Status]
        ,[Created]
        ,[CreatedBy]
        ,[LastModified]
        ,[LastModifiedBy]
FROM [dbo].[Accounts]
/*
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, NULL, N'OtOO6Hk3D8OniUm4H7OqvA==', N'whA/JfmydZIwrx1AkHk68DJ2qQbg/Cqckpm+wuPw4iw=', NULL, N'Active', CAST(N'2024-10-24T15:44:05.7922474' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-24T15:44:05.7922480' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, NULL, N'PSuoA7GOWv8dtEdI8sopIA==', N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=', NULL, N'Active', CAST(N'2024-10-25T12:35:16.0503141' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', CAST(N'2024-10-25T12:35:16.0503153' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, NULL, N'8IhOGNdEbCwdLoYonkiVbq/S9IDWN3urQCL/LFEIkJY=', N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', NULL, N'Active', CAST(N'2024-10-25T15:31:39.2265598' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-25T15:31:39.2265604' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, NULL, N'seNoYDDS2rX1sGu0M5wgCQ==', N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', NULL, N'Active', CAST(N'2024-10-25T15:32:38.3944609' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-10-25T15:32:38.3944611' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (6, NULL, N'XzL+pXz6S5/86bhZvdUU1Q==', N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', NULL, N'Active', CAST(N'2024-10-28T11:13:48.8751829' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-28T11:13:48.8751836' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, NULL, N'GgSD+Qg/a6QIuJEvnLA8zQ==', N'R3sNymgchT+ZMdYNw2RJVUZNhnkCeVxc0wqoL4/QBUO6VxRc53WiYie3CevfEeju', NULL, N'Active', CAST(N'2024-10-28T11:14:20.0040917' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-28T11:14:20.0040920' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, NULL, N'NH8bywCNmYbQRBVX9uyqHA==', N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=', NULL, N'Active', CAST(N'2024-10-31T18:20:03.7684276' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', CAST(N'2024-10-31T18:20:03.7684287' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (9, NULL, N'PPRmvTalZrdDeqU3PsfwisHS0hAsQJzAzjDL0WRkgAM=', N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=', NULL, N'Active', CAST(N'2024-10-31T18:22:58.3531798' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', CAST(N'2024-10-31T18:22:58.3531800' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, NULL, N'8SuwpPCXXZGsnqq27Dr2JGckHxdQ2B++iga+Zd3FBZI=', N'7SJS1u2FuniUmlO+vXlr9mRYXLttA7butpgRGncwSW8gf1vtO6RF0eUhevfbwbt5', NULL, N'Active', CAST(N'2024-11-01T12:13:59.5391867' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:13:59.5391870' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, NULL, N'IMgd580UAbEz3jojbAVsaS8faYPg2pmwGQvAFVmHTpU=', N'G0/TPGD7aAYDlpDfqDIn0w0O3RN1LBxpkDTOaXYaucyWKL/z6dM4Dj+M6/Ysv293', NULL, N'Active', CAST(N'2024-11-01T12:14:28.3616366' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:14:28.3616369' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, NULL, N'dY4ZUGQCiJWWwGE/Tv0ZHGK6gA/aEPfHGGffEAXkI3M=', N'DRa2ELj9F1kWwPxmN6LojDdnCQsJ3tEputM3yrfbcPx0q1VRWViqyJq1vEbS44F/', NULL, N'Active', CAST(N'2024-11-01T12:14:58.3674760' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:14:58.3674768' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (13, NULL, N'Tt5hZrEzsNEwzKmgPJb8YyCG3dSuzWNKDegdV1En30o=', N'cFZIgFXYvuL/ERoXPjZzpK5nQ5J8UZ1eCo/HKGVpXl+HOoEQ7gkRvldpayTxeYvv', NULL, N'Active', CAST(N'2024-11-01T12:15:45.0171743' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:15:45.0171746' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (14, NULL, N'Vqbmq4pTluRpZRBcVR04VW/bqFwBRG/fAWFL/wF7R84=', N'D55D+bvjIZKVmMevLQUuEUe3OlZMrltsVLSy7E9qlmU9qpkJouQva4/59VSoqCk+', NULL, N'Active', CAST(N'2024-11-01T12:16:22.6495801' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:16:22.6495803' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (15, NULL, N'Vqbmq4pTluRpZRBcVR04VYnlZyx5KHe8RjdeHxQX3g8=', N'aAJRffjj87Ko8Q+X3MhlVaTVCQ/Ek0AnuIk1Lfh/8DFAsWD6ItzXpYZDwWrCQ+Xc', NULL, N'Active', CAST(N'2024-11-01T12:17:08.6156172' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:17:08.6156176' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountClaims] ON 

INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, 2, N'role', N'DfeAdmin', CAST(N'2024-10-24T15:44:05.7922503' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-24T15:44:05.7922505' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, 2, N'OrganisationId', N'-1', CAST(N'2024-10-24T15:44:05.7922510' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-24T15:44:05.7922511' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, 2, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/24/2024 3:44:32 PM', CAST(N'2024-10-24T15:44:33.7811362' AS DateTime2), N'whA/JfmydZIwrx1AkHk68DJ2qQbg/Cqckpm+wuPw4iw=', CAST(N'2024-10-24T15:44:33.7811370' AS DateTime2), N'whA/JfmydZIwrx1AkHk68DJ2qQbg/Cqckpm+wuPw4iw=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, 3, N'role', N'VcsDualRole', CAST(N'2024-10-25T12:35:16.0503158' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', CAST(N'2024-10-25T12:35:16.0503202' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, 3, N'OrganisationId', N'11', CAST(N'2024-10-25T12:35:16.0503204' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', CAST(N'2024-10-25T12:35:16.0503206' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, 3, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/25/2024 12:42:59 PM', CAST(N'2024-10-25T12:43:00.0673774' AS DateTime2), N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=', CAST(N'2024-10-25T12:43:00.0673782' AS DateTime2), N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, 3, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/25/2024 12:53:25 PM', CAST(N'2024-10-25T12:53:25.3457669' AS DateTime2), N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=', CAST(N'2024-10-25T12:53:25.3457671' AS DateTime2), N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, 4, N'role', N'DfeAdmin', CAST(N'2024-10-25T15:31:39.2265634' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-25T15:31:39.2265635' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (13, 4, N'OrganisationId', N'-1', CAST(N'2024-10-25T15:31:39.2265639' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-25T15:31:39.2265640' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (14, 4, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/25/2024 3:32:10 PM', CAST(N'2024-10-25T15:32:10.5496167' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-10-25T15:32:10.5496169' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (15, 5, N'role', N'LaDualRole', CAST(N'2024-10-25T15:32:38.3944621' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-10-25T15:32:38.3944622' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (16, 5, N'OrganisationId', N'6', CAST(N'2024-10-25T15:32:38.3944623' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-10-25T15:32:38.3944624' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (17, 5, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/28/2024 10:14:14 AM', CAST(N'2024-10-28T10:14:14.7677663' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', CAST(N'2024-10-28T10:14:14.7677667' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (18, 6, N'role', N'DfeAdmin', CAST(N'2024-10-28T11:13:48.8751879' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-28T11:13:48.8751880' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (19, 6, N'OrganisationId', N'-1', CAST(N'2024-10-28T11:13:48.8751885' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-28T11:13:48.8751886' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (20, 7, N'role', N'DfeAdmin', CAST(N'2024-10-28T11:14:20.0040924' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-28T11:14:20.0040926' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (21, 7, N'OrganisationId', N'-1', CAST(N'2024-10-28T11:14:20.0040928' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-10-28T11:14:20.0040930' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (22, 5, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/28/2024 4:25:35 PM', CAST(N'2024-10-28T16:25:35.8732614' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', CAST(N'2024-10-28T16:25:35.8732619' AS DateTime2), N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (23, 6, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 12:53:29 PM', CAST(N'2024-10-31T12:53:31.0558586' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', CAST(N'2024-10-31T12:53:31.0558592' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (24, 8, N'role', N'LaDualRole', CAST(N'2024-10-31T18:20:03.7684291' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', CAST(N'2024-10-31T18:20:03.7684292' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (25, 8, N'OrganisationId', N'6', CAST(N'2024-10-31T18:20:03.7684293' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', CAST(N'2024-10-31T18:20:03.7684294' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (26, 9, N'role', N'VcsDualRole', CAST(N'2024-10-31T18:22:58.3531802' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', CAST(N'2024-10-31T18:22:58.3531804' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (27, 9, N'OrganisationId', N'11', CAST(N'2024-10-31T18:22:58.3531805' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', CAST(N'2024-10-31T18:22:58.3531806' AS DateTime2), N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (28, 9, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/31/2024 6:25:19 PM', CAST(N'2024-10-31T18:25:19.7975020' AS DateTime2), N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=', CAST(N'2024-10-31T18:25:19.7975024' AS DateTime2), N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (29, 9, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 6:25:56 PM', CAST(N'2024-10-31T18:25:56.8984216' AS DateTime2), N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=', CAST(N'2024-10-31T18:25:56.8984219' AS DateTime2), N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (30, 8, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 6:28:02 PM', CAST(N'2024-10-31T18:28:02.3561203' AS DateTime2), N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=', CAST(N'2024-10-31T18:28:02.3561205' AS DateTime2), N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (31, 8, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'11/1/2024 8:31:25 AM', CAST(N'2024-11-01T08:31:25.5400179' AS DateTime2), N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=', CAST(N'2024-11-01T08:31:25.5400182' AS DateTime2), N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (32, 10, N'role', N'LaManager', CAST(N'2024-11-01T12:13:59.5391873' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:13:59.5391874' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (33, 10, N'OrganisationId', N'6', CAST(N'2024-11-01T12:13:59.5391875' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:13:59.5391876' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (34, 11, N'role', N'LaDualRole', CAST(N'2024-11-01T12:14:28.3616372' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:14:28.3616374' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (35, 11, N'OrganisationId', N'6', CAST(N'2024-11-01T12:14:28.3616375' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:14:28.3616376' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (36, 12, N'role', N'LaProfessional', CAST(N'2024-11-01T12:14:58.3674772' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:14:58.3674773' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (37, 12, N'OrganisationId', N'6', CAST(N'2024-11-01T12:14:58.3674775' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:14:58.3674776' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (38, 13, N'role', N'VcsManager', CAST(N'2024-11-01T12:15:45.0171750' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:15:45.0171751' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (39, 13, N'OrganisationId', N'11', CAST(N'2024-11-01T12:15:45.0171754' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:15:45.0171755' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (40, 14, N'role', N'VcsDualRole', CAST(N'2024-11-01T12:16:22.6495807' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:16:22.6495808' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (41, 14, N'OrganisationId', N'11', CAST(N'2024-11-01T12:16:22.6495810' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:16:22.6495811' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (42, 15, N'role', N'VcsProfessional', CAST(N'2024-11-01T12:17:08.6156180' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:17:08.6156181' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (43, 15, N'OrganisationId', N'11', CAST(N'2024-11-01T12:17:08.6156182' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', CAST(N'2024-11-01T12:17:08.6156184' AS DateTime2), N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
SET IDENTITY_INSERT [dbo].[AccountClaims] OFF
GO
SET IDENTITY_INSERT [dbo].[UserSessions] ON 

INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (78, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', N'GNJkk4oUIV_ngyDFH09w323FzUM', CAST(N'2024-11-01T14:42:15.0871821' AS DateTime2), CAST(N'2024-11-01T14:35:13.3008746' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-11-01T14:42:15.0872428' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (79, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', N'FmGH1G73NI-k_PtT-qCJNMXKYNc', CAST(N'2024-11-01T14:58:37.9681411' AS DateTime2), CAST(N'2024-11-01T14:57:06.5566561' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-11-01T14:58:37.9681965' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (80, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', N'46AoDM2elbXF4qH9mTqfI-3jjj4', CAST(N'2024-11-01T15:16:54.5379494' AS DateTime2), CAST(N'2024-11-01T15:16:51.3232043' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==', CAST(N'2024-11-01T15:16:54.5380239' AS DateTime2), N'WQRvbV5MsPLEexIm97uBMA==')
SET IDENTITY_INSERT [dbo].[UserSessions] OFF
GO

 */
