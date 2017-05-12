--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Cuenta_Crear $$

CREATE PROCEDURE sp_Cuenta_Crear(
	inEmail VARCHAR(256),
	inContrasena VARCHAR(256)
)

BEGIN
	INSERT INTO Cuenta
		(Email, Contrasena)
	VALUES
		(inEmail,inContrasena);
END
$$
