--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Item_Crear $$
CREATE PROCEDURE sp_Autor_Item_Crear (
	inAutor_Id			BIGINT,
	InItem_Id			BIGINT
	)
BEGIN
	INSERT INTO Autor_Item 
	(
		Autor_Id, 
		Item_Id
	) 
	VALUES 
	(
		inAutor_Id, 
		InItem_Id
	);
END $$