--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Comentario_Seleccionar $$

CREATE PROCEDURE sp_Comentario_Seleccionar (
	inId BIGINT
)
BEGIN 
	SELECT 
		Id, Texto, FechaCreacion, Prestamo_Id, Perfil_Id
	FROM
		Comentario
	WHERE
		Id = inId;
END
$$
