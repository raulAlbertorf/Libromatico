--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Cuenta_Seleccionar $$

CREATE PROCEDURE sp_Cuenta_Seleccionar(
	inEmail VARCHAR(256)
)

BEGIN
	SELECT
		Email, 
		Contrasena 
	FROM
		Cuenta
	WHERE
		Email  = inEmail;
END
$$
