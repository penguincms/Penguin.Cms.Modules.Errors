CREATE PROCEDURE ErrorsByKind
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 
		count(*) as Instances, 
		Exception 
	from auditableerrors group by Exception order by count(*) desc
END
