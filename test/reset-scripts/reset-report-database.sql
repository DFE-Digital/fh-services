--
-- This is a report-specific script that just clears the data from certain tables and leaves all the seeded dim tables
-- untouched. Only migrations that have not previously run will be applied to the report database in this case.
-- The reason is that there is a huge volume of seeded time and date data that simply cannot be done via a pipeline in
-- an efficient manner.
--

BEGIN TRY
    BEGIN TRANSACTION

    DELETE FROM [dim].[ConnectionRequestsSentFacts]

    DELETE FROM [dim].[ServiceSearchFacts]

    DELETE FROM [dim].[ServiceSearchesDim]
    
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