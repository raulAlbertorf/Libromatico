--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Perfil_Modificar_Imagen $$

CREATE PROCEDURE sp_Perfil_Modificar_Imagen(
	inId BIGINT UNSIGNED,
	inUrlImagen VARCHAR(256)
)
BEGIN
	UPDATE Perfil 
	SET
	 	UrlImagen  = inUrlImagen
	WHERE
		Id = inId;
END
$$
