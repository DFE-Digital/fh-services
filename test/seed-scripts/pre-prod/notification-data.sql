BEGIN TRY
BEGIN TRANSACTION

    DECLARE @Today DATETIME2
    SET @Today = GETDATE()
        
    DELETE FROM [dbo].[Notified]
    DELETE FROM [dbo].[TokenValues]
    DELETE FROM [dbo].[SentNotifications]
    
    SET IDENTITY_INSERT [dbo].[SentNotifications] ON
    
    INSERT INTO [dbo].[SentNotifications] ([Id], [TemplateId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [ApiKeyType])
    VALUES  (1, N'74acfbed-428e-49d6-b35c-9b6279b4b2ee', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 0),
            (2, N'eee5cb96-8387-4095-a942-dfe4885b4db3', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', 0),
            (3, N'eee5cb96-8387-4095-a942-dfe4885b4db3', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', 0),
            (4, N'74acfbed-428e-49d6-b35c-9b6279b4b2ee', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', 0),
            (5, N'b2bde99f-b8be-4ae5-bd35-f52ed556777f', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', 1),
            (6, N'd460f57c-9c5e-4c33-8420-cdde4fca85c2', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', 1),
            (7, N'77dbf90d-1088-4d2d-a022-215fbfd95d92', @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', 1),
            (8, N'cc0ba892-c9ae-4990-a07d-f38c4062fd59', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', 0),
            (9, N'eee5cb96-8387-4095-a942-dfe4885b4db3', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', 0),
            (10, N'5074a730-74bc-42fd-ad5b-d1100d7f11ca', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', 0),
            (11, N'2bfb0fdd-374f-478f-b842-973466a96efe', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', 0),
            (12, N'74acfbed-428e-49d6-b35c-9b6279b4b2ee', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', 0),
            (13, N'6d5e73b8-5db8-497e-892f-6edd7e5506ec', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', 0),
            (14, N'b2bde99f-b8be-4ae5-bd35-f52ed556777f', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 1),
            (15, N'd460f57c-9c5e-4c33-8420-cdde4fca85c2', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 1),
            (16, N'b2bde99f-b8be-4ae5-bd35-f52ed556777f', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 1),
            (17, N'd460f57c-9c5e-4c33-8420-cdde4fca85c2', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 1),
            (18, N'b2bde99f-b8be-4ae5-bd35-f52ed556777f', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 1),
            (19, N'd460f57c-9c5e-4c33-8420-cdde4fca85c2', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 1);
    
    SET IDENTITY_INSERT [dbo].[SentNotifications] OFF
    
    SET IDENTITY_INSERT [dbo].[Notified] ON
    
    INSERT INTO [dbo].[Notified] ([Id], [NotificationId], [Value], [SentNotificationId], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, 0, N'YBQZHQGgWKWcy8K8jh3Cn1ngiNJN7oDPbNPDB8loOso=', 1, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (2, 0, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 2, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (3, 0, N'VkGxsqee/fwY1KZylAPP76k65JXkUo7tf9ladt/YHNU=', 3, @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0'),
            (4, 0, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', 4, @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0'),
            (5, 0, N'YBQZHQGgWKWcy8K8jh3Cn1ngiNJN7oDPbNPDB8loOso=', 5, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (6, 0, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', 5, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (7, 0, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', 6, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (8, 0, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', 7, @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4='),
            (9, 0, N'YaYvLekONsOwX38D9On8W+KtwLbqgPTLgKHEwN7VvW3yvVjB4NEZn0qvkkyQlihb', 8, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (10, 0, N'eGUsItQ7N/ZRzSUFupTY9NNtrzedQ7qPmjxcp+Ul7bhrOcE4Py35Ywq0kFZqeWVB', 9, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (11, 0, N'7B1PzvQ5PYDfp01Hr5YcF41q4fRWKagsY9f3/iTrpj/ibrPpxjak76KS4AoDzR9C', 10, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (12, 0, N'lupEIK0oEfhJQ+Vz79iCkd4+uQxhGPlqN4FMU9hcwsKk/oEsc0tNdtUW44FStHpO', 11, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (13, 0, N'zX1gYt1Et+WLT67/41El/I/0Urq6t6NYw+O19Femu2sVRqqv/q5UC9VW7r69byyL', 12, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (14, 0, N'Zhkrz4cdEPqbNC+XoXg3EevTRXh9pORHA5eoVHE12aDDaviwMHBPo2abs9gYZW8/', 13, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (15, 0, N'YBQZHQGgWKWcy8K8jh3Cn1ngiNJN7oDPbNPDB8loOso=', 14, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (16, 0, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', 14, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (17, 0, N'zX1gYt1Et+WLT67/41El/I/0Urq6t6NYw+O19Femu2sVRqqv/q5UC9VW7r69byyL', 14, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (18, 0, N'Zhkrz4cdEPqbNC+XoXg3EevTRXh9pORHA5eoVHE12aDDaviwMHBPo2abs9gYZW8/', 14, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (19, 0, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 15, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (20, 0, N'YBQZHQGgWKWcy8K8jh3Cn1ngiNJN7oDPbNPDB8loOso=', 16, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (21, 0, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', 16, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (22, 0, N'zX1gYt1Et+WLT67/41El/I/0Urq6t6NYw+O19Femu2sVRqqv/q5UC9VW7r69byyL', 16, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (23, 0, N'Zhkrz4cdEPqbNC+XoXg3EevTRXh9pORHA5eoVHE12aDDaviwMHBPo2abs9gYZW8/', 16, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (24, 0, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 17, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (25, 0, N'YBQZHQGgWKWcy8K8jh3Cn1ngiNJN7oDPbNPDB8loOso=', 18, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (26, 0, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', 18, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (27, 0, N'zX1gYt1Et+WLT67/41El/I/0Urq6t6NYw+O19Femu2sVRqqv/q5UC9VW7r69byyL', 18, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (28, 0, N'Zhkrz4cdEPqbNC+XoXg3EevTRXh9pORHA5eoVHE12aDDaviwMHBPo2abs9gYZW8/', 18, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (29, 0, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', 19, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=');
    
    SET IDENTITY_INSERT [dbo].[Notified] OFF
    
    SET IDENTITY_INSERT [dbo].[TokenValues] ON
    
    INSERT INTO [dbo].[TokenValues] ([Id], [NotificationId], [Key], [Value], [SentNotificationId], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 1, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (2, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 1, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (3, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 2, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (4, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 2, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (5, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 3, @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0'),
            (6, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 3, @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0'),
            (7, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 4, @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0'),
            (8, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 4, @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0', @Today, N'Xkn75fdrrwIgQWxe/t6OHSLiZcvLEy9ogfr9LnElnrRvlMi4WXtvVGg5GJl5wnF0'),
            (9, 0, N'RequestNumber', N'000001', 5, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (10, 0, N'ServiceName', N'LGBT+ Asylum support', 5, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (11, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/Vcs/RequestDetails?id=1', 5, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (12, 0, N'RequestNumber', N'000001', 6, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (13, 0, N'ServiceName', N'LGBT+ Asylum support', 6, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (14, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=1', 6, @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w=', @Today, N'riBtuSx/xnnXPunFF5KrUFIB/nxdoKc1vGb/TydOm5w='),
            (15, 0, N'RequestNumber', N'000001', 7, @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4='),
            (16, 0, N'ServiceName', N'LGBT+ Asylum support', 7, @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4='),
            (17, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=1', 7, @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4=', @Today, N'FdHgqnSIr+xYGcgrhTc5RspQ0O7NanpzeG+16e5uTk4='),
            (18, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 8, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (19, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 8, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (20, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 9, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (21, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 9, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (22, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 10, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (23, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 10, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (24, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 11, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (25, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 11, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (26, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 12, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (27, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 12, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (28, 0, N'ConnectFamiliesToSupportStartPage', N'https://preprod.connect-families-to-support.education.gov.uk/', 13, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (29, 0, N'ManageFamilySupportServicesStartPage', N'https://preprod.manage-family-support-services-and-accounts.education.gov.uk/', 13, @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7', @Today, N'Pc5hn5dGpeGMpxZO3rKJTHHR3gQPs+uyAnlTjtvtEOEmP6r0M1DRR97wuFpXaxR7'),
            (30, 0, N'RequestNumber', N'000002', 14, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (31, 0, N'ServiceName', N'LGBT+ Asylum support', 14, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (32, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/Vcs/RequestDetails?id=2', 14, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (33, 0, N'RequestNumber', N'000002', 15, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (34, 0, N'ServiceName', N'LGBT+ Asylum support', 15, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (35, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=2', 15, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (36, 0, N'RequestNumber', N'000003', 16, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (37, 0, N'ServiceName', N'Young People’s Services ‘Youth Out East’', 16, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (38, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/Vcs/RequestDetails?id=3', 16, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (39, 0, N'RequestNumber', N'000003', 17, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (40, 0, N'ServiceName', N'Young People’s Services ‘Youth Out East’', 17, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (41, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=3', 17, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (42, 0, N'RequestNumber', N'000004', 18, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (43, 0, N'ServiceName', N'LGBT+ Asylum support', 18, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (44, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/Vcs/RequestDetails?id=4', 18, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (45, 0, N'RequestNumber', N'000004', 19, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (46, 0, N'ServiceName', N'LGBT+ Asylum support', 19, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM='),
            (47, 0, N'ViewConnectionRequestUrl', N'https://preprod.connect-families-to-support.education.gov.uk/referrals/La/RequestDetails?id=4', 19, @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=', @Today, N'cQ1yui0sGH8FStDTZSQXsgpg/a5NNQNKDK/n9h9FitM=');
    
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