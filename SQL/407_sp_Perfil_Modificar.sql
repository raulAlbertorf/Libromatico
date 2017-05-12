--DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Perfil_Modificar $$

CREATE PROCEDURE sp_Perfil_Modificar(
	inId BIGINT UNSIGNED,
	inNombre VARCHAR(256),
	inUrl VARCHAR(256),
	inNacionalidad VARCHAR(256),
	inEmail VARCHAR(256)
)
BEGIN
	UPDATE Perfil 
	SET
	 	Nombre = inNombre,
	 	UrlImagen = inUrl,
	 	Nacionalidad = inNacionalidad,
	 	Cuenta_Email = inEmail
	WHERE
		Id = inId;
END
$$