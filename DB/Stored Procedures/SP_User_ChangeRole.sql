CREATE PROCEDURE [dbo].[SP_User_ChangeRole]
	@user_id UNIQUEIDENTIFIER,
	@role NVARCHAR(8)
AS
BEGIN
	UPDATE [User]
		SET [Role] = @role
		WHERE [User_Id] = @user_id
END
