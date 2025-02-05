/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
-- Ajout des rôles nécessaire à l'application : User et Admin
INSERT INTO [Role] VALUES ('User'),('Admin');

-- Déclaration de la table variable pour stocker les IDs insérés
DECLARE @InsertedUserIds TABLE (UserId UNIQUEIDENTIFIER);

-- Insérer 5 utilisateurs (avec des mots de passe plus complexes pour l'exemple)

INSERT INTO @InsertedUserIds (UserId)
EXEC SP_User_Insert @first_name = N'John', @last_name = N'Doe', @email = N'john.doe@example.com', @password = N'Test1234=';

DECLARE @User1Id UNIQUEIDENTIFIER;
SELECT @User1Id = UserId FROM @InsertedUserIds;
EXEC SP_User_ChangeRole @user_id=@User1Id, @role = N'Admin'
DELETE FROM @InsertedUserIds;

INSERT INTO @InsertedUserIds (UserId)
EXEC SP_User_Insert @first_name = N'Jane', @last_name = N'Smith', @email = N'jane.smith@example.com', @password = N'Test1234=';
DECLARE @User2Id UNIQUEIDENTIFIER;
SELECT @User2Id = UserId FROM @InsertedUserIds;
DELETE FROM @InsertedUserIds;

INSERT INTO @InsertedUserIds (UserId)
EXEC SP_User_Insert @first_name = N'Peter', @last_name = N'Jones', @email = N'peter.jones@example.com', @password = N'Test1234=';
DECLARE @User3Id UNIQUEIDENTIFIER;
SELECT @User3Id = UserId FROM @InsertedUserIds;
DELETE FROM @InsertedUserIds;

INSERT INTO @InsertedUserIds (UserId)
EXEC SP_User_Insert @first_name = N'Alice', @last_name = N'Johnson', @email = N'alice.johnson@example.com', @password = N'Test1234=';
DECLARE @User4Id UNIQUEIDENTIFIER;
SELECT @User4Id = UserId FROM @InsertedUserIds;
DELETE FROM @InsertedUserIds;

INSERT INTO @InsertedUserIds (UserId)
EXEC SP_User_Insert @first_name = N'Bob', @last_name = N'Williams', @email = N'bob.williams@example.com', @password = N'Test1234=';
DECLARE @User5Id UNIQUEIDENTIFIER;
SELECT @User5Id = UserId FROM @InsertedUserIds;
DELETE FROM @InsertedUserIds;


-- Insérer des cocktails pour chaque utilisateur en utilisant les procédures stockées et des exemples réels

-- Cocktails pour John Doe
EXEC SP_Cocktail_Insert @name = N'Mojito', @description = N'Un cocktail cubain rafraîchissant à base de rhum, de menthe, de citron vert et de sucre.', @instructions = N'1. Dans un verre, piler la menthe fraîche avec le sucre et le jus de citron vert.
2. Ajouter le rhum blanc.
3. Remplir le verre de glace pilée.
4. Compléter avec de l''eau gazeuse.
5. Mélanger délicatement et décorer avec un brin de menthe et une tranche de citron vert.', @user_id = @User1Id;

EXEC SP_Cocktail_Insert @name = N'Cosmopolitan', @description = N'Un cocktail classique à base de vodka, de Cointreau, de jus de cranberry et de jus de citron vert.', @instructions = N'1. Verser tous les ingrédients dans un shaker rempli de glace.
2. Secouer énergiquement.
3. Filtrer dans un verre à martini refroidi.
4. Décorer avec un zeste de citron vert.', @user_id = @User1Id;

EXEC SP_Cocktail_Insert @name = N'Margarita', @description = N'Un cocktail mexicain à base de tequila, de Cointreau et de jus de citron vert, servi avec du sel sur le bord du verre.', @instructions = N'1. Frotter le bord d''un verre à margarita avec une tranche de citron vert et tremper dans du sel.
2. Verser la tequila, le Cointreau et le jus de citron vert dans un shaker rempli de glace.
3. Secouer énergiquement.
4. Filtrer dans le verre préparé.', @user_id = @User1Id;

-- Cocktails pour Jane Smith
EXEC SP_Cocktail_Insert @name = N'Old Fashioned', @description = N'Un cocktail classique à base de bourbon, de sucre, d''angostura bitters et d''eau.', @instructions = N'1. Dans un verre à whisky, dissoudre le sucre avec quelques gouttes d''angostura bitters et un peu d''eau.
2. Ajouter le bourbon et quelques glaçons.
3. Remuer délicatement.
4. Décorer avec un zeste d''orange.', @user_id = @User2Id;

EXEC SP_Cocktail_Insert @name = N'Negroni', @description = N'Un cocktail italien amer à base de gin, de Campari et de vermouth rouge.', @instructions = N'1. Verser tous les ingrédients dans un verre rempli de glace.
2. Remuer délicatement.
3. Décorer avec une tranche d''orange.', @user_id = @User2Id;

EXEC SP_Cocktail_Insert @name = N'Daiquiri', @description = N'Un cocktail simple et rafraîchissant à base de rhum blanc, de jus de citron vert et de sucre.', @instructions = N'1. Verser tous les ingrédients dans un shaker rempli de glace.
2. Secouer énergiquement.
3. Filtrer dans un verre à cocktail refroidi.', @user_id = @User2Id;

-- Cocktails pour Peter Jones
EXEC SP_Cocktail_Insert @name = N'Manhattan', @description = N'Un cocktail classique à base de whisky (rye ou bourbon), de vermouth rouge et d''angostura bitters.', @instructions = N'1. Verser tous les ingrédients dans un verre à mélange rempli de glace.
2. Remuer délicatement.
3. Filtrer dans un verre à cocktail refroidi.
4. Décorer avec une cerise au marasquin.', @user_id = @User3Id;

EXEC SP_Cocktail_Insert @name = N'Moscow Mule', @description = N'Un cocktail rafraîchissant à base de vodka, de ginger beer et de jus de citron vert, souvent servi dans une tasse en cuivre.', @instructions = N'1. Remplir une tasse en cuivre de glace.
2. Ajouter la vodka et le jus de citron vert.
3. Compléter avec de la ginger beer.
4. Remuer délicatement.
5. Décorer avec une tranche de citron vert.', @user_id = @User3Id;

EXEC SP_Cocktail_Insert @name = N'Pina Colada', @description = N'Un cocktail tropical à base de rhum blanc, de crème de coco et de jus d''ananas.', @instructions = N'1. Verser tous les ingrédients dans un blender avec de la glace.
2. Mixer jusqu''à obtenir une consistance lisse.
3. Verser dans un verre et décorer avec un morceau d''ananas et une cerise au marasquin.', @user_id = @User3Id;

-- Cocktails pour Alice Johnson
EXEC SP_Cocktail_Insert @name = N'Sex on the Beach', @description = N'Un cocktail fruité à base de vodka, de liqueur de pêche, de jus d''orange et de jus de cranberry.', @instructions = N'1. Verser la vodka et la liqueur de pêche dans un verre rempli de glace.
2. Ajouter les jus d''orange et de cranberry.
3. Remuer délicatement.
4. Décorer avec une tranche d''orange.', @user_id = @User4Id;

EXEC SP_Cocktail_Insert @name = N'Long Island Iced Tea', @description = N'Un cocktail puissant à base de vodka, de rhum blanc, de tequila, de gin, de Cointreau, de jus de citron, de Coca-Cola et de sirop de sucre.', @instructions = N'1. Verser tous les alcools et le jus de citron dans un verre highball rempli de glace.
2. Compléter avec du Coca-Cola.
3. Ajouter un trait de sirop de sucre.
4. Remuer délicatement.
5. Décorer avec une tranche de citron.', @user_id = @User4Id;

EXEC SP_Cocktail_Insert @name = N'Tequila Sunrise', @description = N'Un cocktail coloré à base de tequila, de jus d''orange et de grenadine.', @instructions = N'1. Verser la tequila et le jus d''orange dans un verre rempli de glace.
2. Verser délicatement la grenadine pour qu''elle se dépose au fond du verre.
3. Décorer avec une tranche d''orange.', @user_id = @User4Id;

-- Cocktails pour Bob Williams

EXEC SP_Cocktail_Insert @name = N'French 75', @description = N'Un cocktail pétillant à base de gin, de champagne, de jus de citron et de sucre.', @instructions = N'1. Verser le gin, le jus de citron et le sucre dans un shaker rempli de glace.
2. Secouer énergiquement.
3. Filtrer dans une flûte à champagne.
4. Compléter avec du champagne.', @user_id = @User5Id;

EXEC SP_Cocktail_Insert @name = N'Sidecar', @description = N'Un cocktail classique à base de cognac, de Cointreau et de jus de citron.', @instructions = N'1. Frotter le bord d''un verre à cocktail avec du sucre.
2. Verser le cognac, le Cointreau et le jus de citron dans un shaker rempli de glace.
3. Secouer énergiquement.
4. Filtrer dans le verre préparé.', @user_id = @User5Id;

EXEC SP_Cocktail_Insert @name = N'Sazerac', @description = N'Un cocktail de La Nouvelle-Orléans à base de whisky de seigle, d''absinthe, de sucre et de Peychaud''s Bitters.', @instructions = N'1. Rincer un verre à whisky avec de l''absinthe et jeter l''excédent.
2. Dans un autre verre, mélanger le sucre avec quelques gouttes de Peychaud''s Bitters et un peu d''eau.
3. Ajouter le whisky de seigle et de la glace.
4. Remuer délicatement.
5. Verser le mélange dans le verre préparé.
6. Décorer avec un zeste de citron.', @user_id = @User5Id;