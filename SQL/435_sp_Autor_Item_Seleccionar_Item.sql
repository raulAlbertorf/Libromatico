--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Item_Seleccionar_Item $$
CREATE PROCEDURE sp_Autor_Item_Seleccionar_Item (
	inItem_Id BIGINT
)
BEGIN
	SELECT 	
		A_Item.Id, 
		A_Item.Autor_Id, 
        A_Item.Item_Id,
        A.Nombre AS Autor_Nombre
	FROM 
		Autor_Item AS A_Item,
        Autor AS A
	WHERE 
		A_Item.Item_Id = inItem_Id AND
        A.Id  = A_Item.Autor_ID;
END $$
