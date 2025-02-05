CREATE PROCEDURE [dbo].[SP_User_GetById]
	@user_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[User_Id],
			[First_Name],
			[Last_Name],
			[Email], 
			[CreatedAt], 
			[DisabledAt],
			[Role]
		FROM [User]
		WHERE [User_Id] = @user_id
END
