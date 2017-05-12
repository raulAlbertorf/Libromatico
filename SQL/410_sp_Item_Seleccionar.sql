--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_Seleccionar $$

CREATE PROCEDURE sp_Item_Seleccionar(
	inId BIGINT UNSIGNED
)
BEGIN
	SELECT 
		Id, Titulo, Resumen, VecesVisto, Perfil_Id, UrlImagen
	FROM
		Item
	WHERE
		Id = inId;
END
$$