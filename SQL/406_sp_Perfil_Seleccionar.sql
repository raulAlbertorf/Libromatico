--DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Perfil_Seleccionar $$

CREATE PROCEDURE sp_Perfil_Seleccionar(
	inId VARCHAR(256)
)
BEGIN
	SELECT 
		Id,Nombre,UrlImagen,Nacionalidad,Cuenta_Email
	FROM
		Perfil
	WHERE
		Id = inId;
END

$$