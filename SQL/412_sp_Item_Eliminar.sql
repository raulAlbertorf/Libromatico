--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_Eliminar $$

CREATE PROCEDURE sp_Item_Eliminar(
	inId BIGINT UNSIGNED
)
BEGIN
	DELETE FROM 
		Item
	WHERE
		Id = inId;
END
$$
