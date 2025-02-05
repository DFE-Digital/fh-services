BEGIN TRY
    BEGIN TRANSACTION
    
    DECLARE @Today DATETIME2
    SET @Today = GETDATE()
            
    DELETE FROM [dbo].[Accounts]
    
    SET IDENTITY_INSERT [dbo].[Accounts] ON

    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, NULL, N'iabRAfAjVT8ww86Cr9NBmw==', N'iVQ7rFF4NbBSWFf7EWGzkhEJd1S0miPfxKwpU5F3u5E=', NULL, N'Active', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, NULL, N'VeEXt/mhw2C2HwGOvbtfcw==', N'd1P5x7cMVoWJcVSaRkK87Hz0Fl990t/GL5pKZL6xB/k=', NULL, N'Active', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, NULL, N'Jpv0CKwOYw2gqT4eOsJlqAN9tmxcCVghF45NnId8MMg=', N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', NULL, N'Active', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, NULL, N'4zOseWt0x2f1u8O3AtgopQ==', N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', NULL, N'Active', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (6, NULL, N'xkGMIPQiHCmwEZeol++w6g==', N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU', NULL, N'Active', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, NULL, N'wPjXbl35Nj0orAIjZB0T0g==', N'6sobsh4XvksIYun8OV5jFVSZza6IkUpRBZGSoP3HTCCfoFRxJhJE/e0T1qHtuorJ', NULL, N'Active', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, NULL, N'rNXa2txU6zu+m+r5hHq7qw==', N'KwBUz9LeOal8vUKxPChJVE0NLCovItF4TQwPlt2/OEY=', NULL, N'Active', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (9, NULL, N'YGOKOeSdmZY2E9US/eSzFPlb/6tLhCParEg5yNSLKq0=', N'8ZcSTntxf0HPRJ2hf4Qw5Pw4S0gtqKbkguVaJHH9uoE=', NULL, N'Active', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, NULL, N'2ZBhX16guTXj0MPVog18SqqN53ioi1FmUYHU0+8MNUc=', N'8Sp+yOm0f3g35qGnXCmxKYEVCv/fCMDNa5fnTIdhGS5m+UTlCKyLT59c2GWJvIf0', NULL, N'Active', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, NULL, N'a7I3Tr0G9D/t1rzZYwN/siPsuH8P44b/cGgOmeZWWcE=', N'iA6wHKEdseSNmBOdRWPZE381CpNdy4x5xPxbLNG325zeos+HAacgSOg2Y8C71Xdq', NULL, N'Active', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, NULL, N'7TS/omu+1dAKhS9e+jSCQeLS4mwqQy41HHlUK4elC7Y=', N'eCVZcXSCqhefhDcN425Iu6in4TgzhqfZck2tOq8hi7vUPsoIy4sjJogmQ1M3W9Hx', NULL, N'Active', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (13, NULL, N'uzbjf7ZWChcZOXQQ0wPl1E8x6gcwbfJf7hnIafUEXLQ=', N'7vnp/pd2gbr6fH+9JRPcd3ROqrPhkUIirJN9YbqCwANLGPtb9T4Nv7b1+x58f27y', NULL, N'Active', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (14, NULL, N'pnGFqoHFRvAjppjGb13N/9pmU5v+QzwxLbiiHlhpirc=', N'0FhCHFdgYTxAqYnX9WUUtyQjLtsDFkIRcxEjQ0IR6SSXYi8LLWGCyGJBRv7f6hC0', NULL, N'Active', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (15, NULL, N'pnGFqoHFRvAjppjGb13N/xttZMYpe+qOdgu7K7VfEJY=', N'JRfDy/kOUDJti34VXOmc+fAJjanIjIoPHFYr+XVgjC5IviEWhyn4ojyT6RRsVPcD', NULL, N'Active', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')

    SET IDENTITY_INSERT [dbo].[Accounts] OFF
    
    DELETE FROM [dbo].[AccountClaims]
    
    SET IDENTITY_INSERT [dbo].[AccountClaims] ON

    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (3, 2, N'role', N'DfeAdmin', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (4, 2, N'OrganisationId', N'-1', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (5, 2, N'TermsAndConditionsAccepted-https://dev.manage-family-support-services-and-accounts.education.gov.uk', N'10/24/2024 3:44:32 PM', @Today, N'iVQ7rFF4NbBSWFf7EWGzkhEJd1S0miPfxKwpU5F3u5E=', @Today, N'iVQ7rFF4NbBSWFf7EWGzkhEJd1S0miPfxKwpU5F3u5E=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, 3, N'role', N'VcsDualRole', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, 3, N'OrganisationId', N'11', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, 3, N'TermsAndConditionsAccepted-https://test.connect-families-to-support.education.gov.uk', N'10/25/2024 12:42:59 PM', @Today, N'd1P5x7cMVoWJcVSaRkK87Hz0Fl990t/GL5pKZL6xB/k=', @Today, N'd1P5x7cMVoWJcVSaRkK87Hz0Fl990t/GL5pKZL6xB/k=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, 3, N'TermsAndConditionsAccepted-https://test.manage-family-support-services-and-accounts.education.gov.uk', N'10/25/2024 12:53:25 PM', @Today, N'd1P5x7cMVoWJcVSaRkK87Hz0Fl990t/GL5pKZL6xB/k=', @Today, N'd1P5x7cMVoWJcVSaRkK87Hz0Fl990t/GL5pKZL6xB/k=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, 4, N'role', N'DfeAdmin', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (13, 4, N'OrganisationId', N'-1', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (14, 4, N'TermsAndConditionsAccepted-https://dev.manage-family-support-services-and-accounts.education.gov.uk', N'10/25/2024 3:32:10 PM', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (15, 5, N'role', N'LaDualRole', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (16, 5, N'OrganisationId', N'6', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (17, 5, N'TermsAndConditionsAccepted-https://dev.connect-families-to-support.education.gov.uk', N'10/28/2024 10:14:14 AM', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (18, 6, N'role', N'DfeAdmin', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (19, 6, N'OrganisationId', N'-1', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (20, 7, N'role', N'DfeAdmin', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (21, 7, N'OrganisationId', N'-1', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (22, 5, N'TermsAndConditionsAccepted-https://dev.manage-family-support-services-and-accounts.education.gov.uk', N'10/28/2024 4:25:35 PM', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', @Today, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (23, 6, N'TermsAndConditionsAccepted-https://dev.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 12:53:29 PM', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (24, 8, N'role', N'LaDualRole', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (25, 8, N'OrganisationId', N'6', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (26, 9, N'role', N'VcsDualRole', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (27, 9, N'OrganisationId', N'11', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU', @Today, N'6sobsh4XvksIYun8OV5jFaxfPI26I+DUWdBkeejixNLKTd3//5umyuYI7CGeUHEU')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (28, 9, N'TermsAndConditionsAccepted-https://dev.connect-families-to-support.education.gov.uk', N'10/31/2024 6:25:19 PM', @Today, N'8ZcSTntxf0HPRJ2hf4Qw5Pw4S0gtqKbkguVaJHH9uoE=', @Today, N'8ZcSTntxf0HPRJ2hf4Qw5Pw4S0gtqKbkguVaJHH9uoE=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (29, 9, N'TermsAndConditionsAccepted-https://dev.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 6:25:56 PM', @Today, N'8ZcSTntxf0HPRJ2hf4Qw5Pw4S0gtqKbkguVaJHH9uoE=', @Today, N'8ZcSTntxf0HPRJ2hf4Qw5Pw4S0gtqKbkguVaJHH9uoE=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (30, 8, N'TermsAndConditionsAccepted-https://dev.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 6:28:02 PM', @Today, N'KwBUz9LeOal8vUKxPChJVE0NLCovItF4TQwPlt2/OEY=', @Today, N'KwBUz9LeOal8vUKxPChJVE0NLCovItF4TQwPlt2/OEY=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (31, 8, N'TermsAndConditionsAccepted-https://dev.connect-families-to-support.education.gov.uk', N'11/1/2024 8:31:25 AM', @Today, N'KwBUz9LeOal8vUKxPChJVE0NLCovItF4TQwPlt2/OEY=', @Today, N'KwBUz9LeOal8vUKxPChJVE0NLCovItF4TQwPlt2/OEY=')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (32, 10, N'role', N'LaManager', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (33, 10, N'OrganisationId', N'6', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (34, 11, N'role', N'LaDualRole', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (35, 11, N'OrganisationId', N'6', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (36, 12, N'role', N'LaProfessional', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (37, 12, N'OrganisationId', N'6', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (38, 13, N'role', N'VcsManager', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (39, 13, N'OrganisationId', N'11', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (40, 14, N'role', N'VcsDualRole', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (41, 14, N'OrganisationId', N'11', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (42, 15, N'role', N'VcsProfessional', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')
    INSERT [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (43, 15, N'OrganisationId', N'11', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq', @Today, N'lvIIqRdACVXhpKe/wrcV4CPJwM3pIq5bPfXTqeFf/F+1HfmAEnLb4qgrTyUvikeq')

    SET IDENTITY_INSERT [dbo].[AccountClaims] OFF
    
    DELETE FROM [dbo].[UserSessions]
    
    SET IDENTITY_INSERT [dbo].[UserSessions] ON

    INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (78, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', N'GNJkk4oUIV_ngyDFH09w323FzUM', @Today, @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (79, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', N'FmGH1G73NI-k_PtT-qCJNMXKYNc', @Today, @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')
    INSERT [dbo].[UserSessions] ([Id], [Email], [Sid], [LastActive], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (80, N'uNx83HufUdm62nKf+Nwx6HWjH95Ie3KFcAMSS2plTsk=', N'46AoDM2elbXF4qH9mTqfI-3jjj4', @Today, @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==', @Today, N'qJoVFc2JYaGTxHcYmlgGMQ==')

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
