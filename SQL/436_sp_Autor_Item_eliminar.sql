--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Item_Eliminar $$
CREATE PROCEDURE sp_Autor_Item_Eliminar (
	inAutor_Id			BIGINT,
	InItem_Id			BIGINT
	)
BEGIN
	DELETE FROM 
		Autor_Item
	  WHERE
		Autor_Id = inAutor_Id AND
	    Item_Id = InItem_Id;
END $$