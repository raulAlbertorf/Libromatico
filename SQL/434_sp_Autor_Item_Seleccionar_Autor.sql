--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Item_Seleccionar_Autor $$
CREATE PROCEDURE sp_Autor_Item_Seleccionar_Autor (
	inAutor_Id BIGINT
	)
BEGIN
	SELECT 	
		Id, 
		Autor_Id, 
        Item_Id
	FROM 
		Autor_Item
	WHERE 
		Autor_Id = inAutor_Id;
END $$
