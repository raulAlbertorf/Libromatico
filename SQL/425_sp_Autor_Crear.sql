--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Crear $$

CREATE PROCEDURE sp_Autor_Crear (
	inNombre VARCHAR(255),
	OUT outId BIGINT
)
BEGIN
	INSERT INTO Autor
		(Nombre)
	VALUES 
		(InNombre);

	SET outId = LAST_INSERT_ID();
END
$$