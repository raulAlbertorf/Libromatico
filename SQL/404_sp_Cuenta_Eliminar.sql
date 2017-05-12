--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Cuenta_Eliminar $$

CREATE PROCEDURE sp_Cuenta_Eliminar(
	inEmail VARCHAR(256)
)

BEGIN		
	DELETE FROM
		Cuenta
	WHERE
		Email = inEmail;
END
$$
