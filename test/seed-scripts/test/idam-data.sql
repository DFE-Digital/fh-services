BEGIN TRY
    BEGIN TRANSACTION
    
    DECLARE @Today DATETIME2
    SET @Today = GETDATE()
            
    DELETE FROM [dbo].[Accounts]
    
    SET IDENTITY_INSERT [dbo].[Accounts] ON

    INSERT INTO [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (2, NULL, N'yaa00OLK3jXnAf5HVicvsg==', N'dGR0b9aDGM9+4/ORNKcJsDMqDvWVdW1nPkCeR81J7FE=', NULL, N'Active', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (3, NULL, N'xUc/YRhhZ4oA3eAwNcGonw==', N'EcLr19LtjU7WFwIWoz4+kbcVknVP5UoLaNnDydKq+FE=', NULL, N'Active', @Today, N'pskBJgIwB0gn9XvuC5h09leHa6RDZbkiUrr23Gx4myo=', @Today, N'pskBJgIwB0gn9XvuC5h09leHa6RDZbkiUrr23Gx4myo='),
        (4, NULL, N'IidOY1kQxQFppw1UHqWtn10u1gcCbomy0Gc2nLbiLNE=', N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', NULL, N'Active', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (5, NULL, N'Q07GmZd2VX+lAKGLM1srPg==', N'pskBJgIwB0gn9XvuC5h09leHa6RDZbkiUrr23Gx4myo=', NULL, N'Active', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (6, NULL, N'QojZxhLLgKl0/C19MnPjQw==', N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa', NULL, N'Active', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (7, NULL, N'H7FfcEzgp34RIQ2PkmJjag==', N'1nJfZIXcbdqTFEKH3QHxz7Pb8NYJUrdnfSQ2wFAes0mi8get/J1tWXq/HrybA1B0', NULL, N'Active', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (8, NULL, N'x5EVczdVHdYHSgOYqsWHKg==', N'ub7RLmlPDgjtkaNYAfRD2Q6EFcZFX2bldI7JFH0XBDg=', NULL, N'Active', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa'),
        (9, NULL, N'OKwCOUxdkQnUKSAfZkREKDhbiKjtA0imnLB969AFQtg=', N'UnlBEUt/89sTGF05gKO7vtfRCxm7PDVoqlg2EIPEiRU=', NULL, N'Active', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa'),
        (10, NULL, N'GGHvAnr1wbLQHGt3UB34j08XmVLhHMXYInXqWd8+eqE=', N'pD0JcMRdUwfxaJjlR/VOnSykxwBDAu6R7dmFHv4kbXvLpGxsDWiMkmsI/8OKVDR3', NULL, N'Active', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (11, NULL, N'PuVG+wWC1NaLJvGq6c4MKcgipmb59EATRKel1rtf8oQ=', N'7R/7xiIIuHlw8IVdkyuXgKrXoX1lytVcIEYpisjC4qJfm3B/xgok65S3icT1J+mZ', NULL, N'Active', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (12, NULL, N'nTWJdibeNUccAh/k5/oEVjU4iONd8buTddkg41MmPQ0=', N'8YckjpXg7v+tcRKQTZoXxjwriim3QxeK1Z5mO04hxEgxotXn3nYsP3ULYgrvUBtD', NULL, N'Active', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (13, NULL, N'Fod9VnQDeeCufIUYc0mxgFI9VVmfhVVGkAxKqDeBXpM=', N'ThIMRk/CP54VCNxzJ3rcqbtjwJ4izeIqK7AllmcLpRO1PjvV2Nhcd5JDDvs47sxs', NULL, N'Active', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (14, NULL, N'74HaD/wcd93KWsGY/td3gblRujO8AzYJ/khwRH8KGpM=', N'rw8jY9txb12tyBf75+hAgfYjzRREGtRjc6jgZK36WOeW4QHVkliT2lnK+nr9+sNH', NULL, N'Active', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (15, NULL, N'74HaD/wcd93KWsGY/td3gdHXfw27z5TzQ4qrtyaxdC4=', N'2teFKRgF265IbXJj49PqO0co2AWPO18cZ8eiKZTQbV7jld6AZm15UUiPgg81TAJn', NULL, N'Active', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY');
    
    SET IDENTITY_INSERT [dbo].[Accounts] OFF
    
    DELETE FROM [dbo].[AccountClaims]
    
    SET IDENTITY_INSERT [dbo].[AccountClaims] ON

    INSERT INTO [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (1, 2, N'role', N'DfeAdmin', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (2, 2, N'OrganisationId', N'-1', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (3, 3, N'role', N'VcsDualRole', @Today, N'pskBJgIwB0gn9XvuC5h09leHa6RDZbkiUrr23Gx4myo=', @Today, N'pskBJgIwB0gn9XvuC5h09leHa6RDZbkiUrr23Gx4myo='),
        (4, 3, N'OrganisationId', N'11', @Today, N'pskBJgIwB0gn9XvuC5h09leHa6RDZbkiUrr23Gx4myo=', @Today, N'pskBJgIwB0gn9XvuC5h09leHa6RDZbkiUrr23Gx4myo='),
        (5, 4, N'role', N'DfeAdmin', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (6, 4, N'OrganisationId', N'-1', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (7, 5, N'role', N'LaDualRole', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (8, 5, N'OrganisationId', N'6', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (9, 6, N'role', N'DfeAdmin', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (10, 6, N'OrganisationId', N'-1', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (11, 7, N'role', N'DfeAdmin', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (12, 7, N'OrganisationId', N'-1', @Today, N'H3tY5FIbWTThOQ0lJahsxQ==', @Today, N'H3tY5FIbWTThOQ0lJahsxQ=='),
        (13, 8, N'role', N'LaDualRole', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa'),
        (14, 8, N'OrganisationId', N'6', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa'),
        (15, 9, N'role', N'VcsDualRole', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa'),
        (16, 9, N'OrganisationId', N'11', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa', @Today, N'1nJfZIXcbdqTFEKH3QHxz/eH/5NniyKtmX0+DsMYid08CS8zKbGpDaNMzlgldFYa'),
        (17, 10, N'role', N'LaManager', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (18, 10, N'OrganisationId', N'6', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (19, 11, N'role', N'LaDualRole', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (20, 11, N'OrganisationId', N'6', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (21, 12, N'role', N'LaProfessional', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (22, 12, N'OrganisationId', N'6', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (23, 13, N'role', N'VcsManager', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (24, 13, N'OrganisationId', N'11', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (25, 14, N'role', N'VcsDualRole', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (26, 14, N'OrganisationId', N'11', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (27, 15, N'role', N'VcsProfessional', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY'),
        (28, 15, N'OrganisationId', N'11', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY', @Today, N'eZQfJ5iL1GaAbMy9iuoppha58UEvVhbXJsTtoYk/sXoTY3cWrEDKG5mMSvvoZsOY')
    
    SET IDENTITY_INSERT [dbo].[AccountClaims] OFF
    
    DELETE FROM [dbo].[UserSessions]
    
    SET IDENTITY_INSERT [dbo].[UserSessions] ON

   -- No user session data which does make sense
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
