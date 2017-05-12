--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Seleccionar $$

CREATE PROCEDURE sp_Autor_Seleccionar (
	inId BIGINT
)
BEGIN
	SELECT
		Id, Nombre
	FROM
		Autor
	WHERE
		Id = inId;
END
$$