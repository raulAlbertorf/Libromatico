--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_Visitado $$

CREATE PROCEDURE sp_Item_Visitado (
	inId BIGINT
)
BEGIN
	UPDATE Item
	SET
		VecesVisto = VecesVisto + 1
	WHERE
		Item.Id = inId;
END
$$

