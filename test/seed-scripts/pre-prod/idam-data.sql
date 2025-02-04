BEGIN TRY
    BEGIN TRANSACTION
    
    DECLARE @Today DATETIME2
    SET @Today = GETDATE()

    DELETE FROM [dbo].[Accounts]
    
    SET IDENTITY_INSERT [dbo].[Accounts] ON 
    
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, NULL, N'OtOO6Hk3D8OniUm4H7OqvA==', N'whA/JfmydZIwrx1AkHk68DJ2qQbg/Cqckpm+wuPw4iw=', NULL, N'Active', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, NULL, N'PSuoA7GOWv8dtEdI8sopIA==', N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=', NULL, N'Active', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, NULL, N'8IhOGNdEbCwdLoYonkiVbq/S9IDWN3urQCL/LFEIkJY=', N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', NULL, N'Active', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, NULL, N'seNoYDDS2rX1sGu0M5wgCQ==', N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', NULL, N'Active', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (6, NULL, N'XzL+pXz6S5/86bhZvdUU1Q==', N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', NULL, N'Active', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, NULL, N'GgSD+Qg/a6QIuJEvnLA8zQ==', N'R3sNymgchT+ZMdYNw2RJVUZNhnkCeVxc0wqoL4/QBUO6VxRc53WiYie3CevfEeju', NULL, N'Active', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, NULL, N'NH8bywCNmYbQRBVX9uyqHA==', N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=', NULL, N'Active', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (9, NULL, N'PPRmvTalZrdDeqU3PsfwisHS0hAsQJzAzjDL0WRkgAM=', N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=', NULL, N'Active', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, NULL, N'8SuwpPCXXZGsnqq27Dr2JGckHxdQ2B++iga+Zd3FBZI=', N'7SJS1u2FuniUmlO+vXlr9mRYXLttA7butpgRGncwSW8gf1vtO6RF0eUhevfbwbt5', NULL, N'Active', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, NULL, N'IMgd580UAbEz3jojbAVsaS8faYPg2pmwGQvAFVmHTpU=', N'G0/TPGD7aAYDlpDfqDIn0w0O3RN1LBxpkDTOaXYaucyWKL/z6dM4Dj+M6/Ysv293', NULL, N'Active', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, NULL, N'dY4ZUGQCiJWWwGE/Tv0ZHGK6gA/aEPfHGGffEAXkI3M=', N'DRa2ELj9F1kWwPxmN6LojDdnCQsJ3tEputM3yrfbcPx0q1VRWViqyJq1vEbS44F/', NULL, N'Active', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (13, NULL, N'Tt5hZrEzsNEwzKmgPJb8YyCG3dSuzWNKDegdV1En30o=', N'cFZIgFXYvuL/ERoXPjZzpK5nQ5J8UZ1eCo/HKGVpXl+HOoEQ7gkRvldpayTxeYvv', NULL, N'Active', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (14, NULL, N'Vqbmq4pTluRpZRBcVR04VW/bqFwBRG/fAWFL/wF7R84=', N'D55D+bvjIZKVmMevLQUuEUe3OlZMrltsVLSy7E9qlmU9qpkJouQva4/59VSoqCk+', NULL, N'Active', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (15, NULL, N'Vqbmq4pTluRpZRBcVR04VYnlZyx5KHe8RjdeHxQX3g8=', N'aAJRffjj87Ko8Q+X3MhlVaTVCQ/Ek0AnuIk1Lfh/8DFAsWD6ItzXpYZDwWrCQ+Xc', NULL, N'Active', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')

    SET IDENTITY_INSERT [dbo].[Accounts] OFF
    
    DELETE FROM [dbo].[AccountClaims]
    
    SET IDENTITY_INSERT [dbo].[AccountClaims] ON 
    
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, 2, N'role', N'DfeAdmin', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, 2, N'OrganisationId', N'-1', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, 2, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/24/2024 3:44:32 PM', @Today, N'whA/JfmydZIwrx1AkHk68DJ2qQbg/Cqckpm+wuPw4iw=', @Today, N'whA/JfmydZIwrx1AkHk68DJ2qQbg/Cqckpm+wuPw4iw=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, 3, N'role', N'VcsDualRole', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, 3, N'OrganisationId', N'11', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, 3, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/25/2024 12:42:59 PM', @Today, N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=', @Today, N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, 3, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/25/2024 12:53:25 PM', @Today, N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=', @Today, N'e14LaDFadw1Aru4gqEMIp1a+GWu+MewBZU/eArTyCd8=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, 4, N'role', N'DfeAdmin', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (13, 4, N'OrganisationId', N'-1', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (14, 4, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/25/2024 3:32:10 PM', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (15, 5, N'role', N'LaDualRole', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (16, 5, N'OrganisationId', N'6', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (17, 5, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/28/2024 10:14:14 AM', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (18, 6, N'role', N'DfeAdmin', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (19, 6, N'OrganisationId', N'-1', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (20, 7, N'role', N'DfeAdmin', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (21, 7, N'OrganisationId', N'-1', @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (22, 5, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/28/2024 4:25:35 PM', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', @Today, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (23, 6, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 12:53:29 PM', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (24, 8, N'role', N'LaDualRole', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (25, 8, N'OrganisationId', N'6', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (26, 9, N'role', N'VcsDualRole', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (27, 9, N'OrganisationId', N'11', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9', @Today, N'R3sNymgchT+ZMdYNw2RJVSIlCaH/nwyrNIa6oQVwdLBVv9m2LMmNX5XCKDhjyCE9')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (28, 9, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/31/2024 6:25:19 PM', @Today, N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=', @Today, N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (29, 9, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 6:25:56 PM', @Today, N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=', @Today, N'5lzJiCoKmznu3Tv4tOIxh78gGBjGanviCuAvV88rsJI=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (30, 8, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 6:28:02 PM', @Today, N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=', @Today, N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (31, 8, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'11/1/2024 8:31:25 AM', @Today, N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=', @Today, N'pJJIqAerjwgt63YpxJN3/KPH3j6J8O5S8Zh7yFOrfs4=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (32, 10, N'role', N'LaManager', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (33, 10, N'OrganisationId', N'6', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (34, 11, N'role', N'LaDualRole', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (35, 11, N'OrganisationId', N'6', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (36, 12, N'role', N'LaProfessional', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (37, 12, N'OrganisationId', N'6', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (38, 13, N'role', N'VcsManager', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (39, 13, N'OrganisationId', N'11', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (40, 14, N'role', N'VcsDualRole', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (41, 14, N'OrganisationId', N'11', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (42, 15, N'role', N'VcsProfessional', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (43, 15, N'OrganisationId', N'11', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/', @Today, N'XOxYAYXH3tqZqdzMq3NSH3hIr9s8mSfx0V9J7GOH7hIReY/8TQtW5hJ91efzXlm/')

    SET IDENTITY_INSERT [dbo].[AccountClaims] OFF
    
    DELETE FROM [dbo].[UserSessions]
    
    SET IDENTITY_INSERT [dbo].[UserSessions] ON 
    
    INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (78, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', N'GNJkk4oUIV_ngyDFH09w323FzUM', @Today, @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (79, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', N'FmGH1G73NI-k_PtT-qCJNMXKYNc', @Today, @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')
    INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (80, N'M7lUjYLiLMfxnnLwr5xEDYTPqfrc7iD0GqguPldSVW4=', N'46AoDM2elbXF4qH9mTqfI-3jjj4', @Today, @Today, N'WQRvbV5MsPLEexIm97uBMA==', @Today, N'WQRvbV5MsPLEexIm97uBMA==')

    SET IDENTITY_INSERT [dbo].[UserSessions] OFF
    
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
