--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Cuenta_Inicio_Sesion $$
CREATE PROCEDURE sp_Cuenta_Inicio_Sesion (
	inEmail VARCHAR(256),
	inContrasena VARCHAR(256)
)
BEGIN
	SELECT
		Email,
		Contrasena
	FROM 
		Cuenta
	WHERE
		Email = inEmail AND Contrasena = inContrasena;
END $$

