--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Cuenta_Modificar $$

CREATE PROCEDURE sp_Cuenta_Modificar(
	inEmail VARCHAR(256),
	inContrasena VARCHAR(256)
)

BEGIN
	UPDATE Cuenta	
	SET
		Contrasena = inContrasena
	WHERE
		Email = inEmail;
END
$$
