BEGIN TRY
BEGIN TRANSACTION

    DECLARE @Today DATETIME2
    SET @Today = GETDATE()
        
    DELETE FROM [dbo].[Notified]
    DELETE FROM [dbo].[TokenValues]
    DELETE FROM [dbo].[SentNotifications]
    
    SET IDENTITY_INSERT [dbo].[SentNotifications] ON

    INSERT INTO [dbo].[SentNotifications] ([Id], [TemplateId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ApiKeyType])
    VALUES  (1, N'74acfbed-428e-49d6-b35c-9b6279b4b2ee', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 0),
            (2, N'eee5cb96-8387-4095-a942-dfe4885b4db3', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', 0),
            (3, N'eee5cb96-8387-4095-a942-dfe4885b4db3', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', 0),
            (4, N'74acfbed-428e-49d6-b35c-9b6279b4b2ee', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', 0),
            (5, N'b2bde99f-b8be-4ae5-bd35-f52ed556777f', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', 1),
            (6, N'd460f57c-9c5e-4c33-8420-cdde4fca85c2', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', 1),
            (7, N'77dbf90d-1088-4d2d-a022-215fbfd95d92', @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', 1),
            (8, N'cc0ba892-c9ae-4990-a07d-f38c4062fd59', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', 0),
            (9, N'eee5cb96-8387-4095-a942-dfe4885b4db3', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', 0),
            (10, N'5074a730-74bc-42fd-ad5b-d1100d7f11ca', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', 0),
            (11, N'2bfb0fdd-374f-478f-b842-973466a96efe', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', 0),
            (12, N'74acfbed-428e-49d6-b35c-9b6279b4b2ee', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', 0),
            (13, N'6d5e73b8-5db8-497e-892f-6edd7e5506ec', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', 0),
            (14, N'b2bde99f-b8be-4ae5-bd35-f52ed556777f', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 1),
            (15, N'd460f57c-9c5e-4c33-8420-cdde4fca85c2', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 1),
            (16, N'b2bde99f-b8be-4ae5-bd35-f52ed556777f', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 1),
            (17, N'd460f57c-9c5e-4c33-8420-cdde4fca85c2', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 1),
            (18, N'b2bde99f-b8be-4ae5-bd35-f52ed556777f', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 1),
            (19, N'd460f57c-9c5e-4c33-8420-cdde4fca85c2', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 1);

    SET IDENTITY_INSERT [dbo].[SentNotifications] OFF

    SET IDENTITY_INSERT [dbo].[Notified] ON
    
    INSERT INTO [dbo].[Notified] ([Id], [NotificationId], [Value], [SentNotificationId], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, 0, N'7mVF+hFscNTDlHODLlJp8n4K23MVramrw5JyFVVCJiE=', 1, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (2, 0, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 2, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (3, 0, N'WJLrVi3oRsZZbpIEvG0lgjDnlwX5GygaxeFVYeA3Lhs=', 3, @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh'),
            (4, 0, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', 4, @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh'),
            (5, 0, N'7mVF+hFscNTDlHODLlJp8n4K23MVramrw5JyFVVCJiE=', 5, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (6, 0, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', 5, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (7, 0, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', 6, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (8, 0, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', 7, @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM='),
            (9, 0, N'ikposWKzvoTM2edHjcfBXXHqqKRHPzISMr2mUJWNFFnQsotBbL/iVAiObQsQ9aga', 8, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (10, 0, N'TaVZmYS8YkpzIIGZyFhI0nSo41lQHMOSS9Eck08VlQcl7W8Ino8ktZtyBpB1Cuwa', 9, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (11, 0, N'3sUO0ZA8S9zDizckKkA9LdC2asKrvqrJkbczPmWyA030peVhCZWGUeeeqxdgleQ1', 10, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (12, 0, N'D3GJo0Sp6Wa6A6qpPlnAIbj3YueJHHY2YmJaOecWAYUBiPaKoUEYD58fbRUlNhrm', 11, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (13, 0, N'53Nqfyx4zLTgaSdfe9IODy/YwWMSi8aDmRZYsIQ1wAXvrqhuJv4RWMtqXKPce+y0', 12, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (14, 0, N'qRFYIAqaITbUEcQ1AAJlfjOyf18NRgG5o/W/POejnD6T/SR5Q1reco9syM4dNLmh', 13, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (15, 0, N'7mVF+hFscNTDlHODLlJp8n4K23MVramrw5JyFVVCJiE=', 14, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (16, 0, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', 14, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (17, 0, N'53Nqfyx4zLTgaSdfe9IODy/YwWMSi8aDmRZYsIQ1wAXvrqhuJv4RWMtqXKPce+y0', 14, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (18, 0, N'qRFYIAqaITbUEcQ1AAJlfjOyf18NRgG5o/W/POejnD6T/SR5Q1reco9syM4dNLmh', 14, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (19, 0, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 15, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (20, 0, N'7mVF+hFscNTDlHODLlJp8n4K23MVramrw5JyFVVCJiE=', 16, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (21, 0, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', 16, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (22, 0, N'53Nqfyx4zLTgaSdfe9IODy/YwWMSi8aDmRZYsIQ1wAXvrqhuJv4RWMtqXKPce+y0', 16, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (23, 0, N'qRFYIAqaITbUEcQ1AAJlfjOyf18NRgG5o/W/POejnD6T/SR5Q1reco9syM4dNLmh', 16, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (24, 0, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 17, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (25, 0, N'7mVF+hFscNTDlHODLlJp8n4K23MVramrw5JyFVVCJiE=', 18, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (26, 0, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', 18, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (27, 0, N'53Nqfyx4zLTgaSdfe9IODy/YwWMSi8aDmRZYsIQ1wAXvrqhuJv4RWMtqXKPce+y0', 18, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (28, 0, N'qRFYIAqaITbUEcQ1AAJlfjOyf18NRgG5o/W/POejnD6T/SR5Q1reco9syM4dNLmh', 18, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (29, 0, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', 19, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=');
    SET IDENTITY_INSERT dbo.Notified OFF;

    SET IDENTITY_INSERT [dbo].[Notified] OFF
    
    SET IDENTITY_INSERT [dbo].[TokenValues] ON
    
    INSERT INTO [dbo].[TokenValues] ([Id], [NotificationId], [Key], [Value], [SentNotificationId], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 1, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (2, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 1, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (3, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 2, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (4, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 2, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (5, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 3, @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh'),
            (6, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 3, @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh'),
            (7, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 4, @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh'),
            (8, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 4, @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh', @Today, N'I+cPqvnxD8MshDBaDKKFw+dx+HXZBTfqZH8sO9oii9MDW2+nCAsSFfFiF9E65WBh'),
            (9, 0, N'RequestNumber', N'000001', 5, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (10, 0, N'ServiceName', N'LGBT+ Asylum support', 5, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (11, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/Vcs/RequestDetails?id=1', 5, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (12, 0, N'RequestNumber', N'000001', 6, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (13, 0, N'ServiceName', N'LGBT+ Asylum support', 6, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (14, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=1', 6, @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w=', @Today, N'nFkYPrE8veK1iH1QjVI1wWH5ORbrlWAfrRygDub6P6w='),
            (15, 0, N'RequestNumber', N'000001', 7, @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM='),
            (16, 0, N'ServiceName', N'LGBT+ Asylum support', 7, @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM='),
            (17, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=1', 7, @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM=', @Today, N'z0nSpcLbzN+Vj6v0ADhYsJYo+bWNPOEZt1q8WHEaYTM='),
            (18, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 8, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (19, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 8, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (20, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 9, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (21, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 9, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (22, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 10, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (23, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 10, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (24, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 11, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (25, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 11, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (26, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 12, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (27, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 12, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (28, 0, N'ConnectFamiliesToSupportStartPage', N'https://dev.connect-families-to-support.education.gov.uk/', 13, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (29, 0, N'ManageFamilySupportServicesStartPage', N'https://dev.manage-family-support-services-and-accounts.education.gov.uk/', 13, @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz', @Today, N'+kzyXxKfhIm4qQRApkKqiU1KPOg0ME0AVf+P//FhugU5C0Bw2R/pgFnPK+QGfbsz'),
            (30, 0, N'RequestNumber', N'000002', 14, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (31, 0, N'ServiceName', N'LGBT+ Asylum support', 14, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (32, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/Vcs/RequestDetails?id=2', 14, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (33, 0, N'RequestNumber', N'000002', 15, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (34, 0, N'ServiceName', N'LGBT+ Asylum support', 15, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (35, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=2', 15, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (36, 0, N'RequestNumber', N'000003', 16, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (37, 0, N'ServiceName', N'Young People’s Services ‘Youth Out East’', 16, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (38, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/Vcs/RequestDetails?id=3', 16, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (39, 0, N'RequestNumber', N'000003', 17, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (40, 0, N'ServiceName', N'Young People’s Services ‘Youth Out East’', 17, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (41, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=3', 17, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (42, 0, N'RequestNumber', N'000004', 18, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (43, 0, N'ServiceName', N'LGBT+ Asylum support', 18, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (44, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/Vcs/RequestDetails?id=4', 18, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (45, 0, N'RequestNumber', N'000004', 19, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (46, 0, N'ServiceName', N'LGBT+ Asylum support', 19, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM='),
            (47, 0, N'ViewConnectionRequestUrl', N'https://dev.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=4', 19, @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=', @Today, N'lj29w2V/ZnVl+tDHyDeIY+O/+oD3bxJbazc4PJSTckM=');

    SET IDENTITY_INSERT [dbo].[TokenValues] OFF

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