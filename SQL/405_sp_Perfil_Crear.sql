--DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Perfil_Crear $$

CREATE PROCEDURE sp_Perfil_Crear(
	inNombre VARCHAR(256),
	inUrl VARCHAR(256),
	inNacionalidad VARCHAR(256),
	inEmail VARCHAR(256),
	OUT outId BIGINT
)
BEGIN
	INSERT INTO Perfil 
		(Nombre,UrlImagen,Nacionalidad,Cuenta_Email)
	VALUES
		(inNombre,inUrl,inNacionalidad,inEmail);
	SET outId = LAST_INSERT_ID();
END
$$