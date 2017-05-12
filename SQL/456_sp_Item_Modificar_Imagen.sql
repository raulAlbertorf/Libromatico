--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_Modificar_Imagen $$

CREATE PROCEDURE sp_Item_Modificar_Imagen(
	inId BIGINT UNSIGNED,
	inUrlImagen VARCHAR(256)
)
BEGIN
	UPDATE Item 
	SET
	 	UrlImagen  = inUrlImagen
	WHERE
		Id = inId;
END
$$
