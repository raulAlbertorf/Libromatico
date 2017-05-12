--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Prestamo_Seleccionar_Comentarios $$

CREATE PROCEDURE sp_Prestamo_Seleccionar_Comentarios (
	inPrestamo_Id BIGINT
)
BEGIN 
	SELECT
		Com_Pre.Perfil_Id AS Perfil_Id,
		Perfil.Nombre AS Perfil_Nombre,
		Perfil.UrlImagen AS Perfil_URL,
		Com_Pre.Comentario_Id AS Comentario_Id,
		Com_Pre.Comentario_Texto AS Comentario_Texto,
		Com_Pre.Comentario_Fecha AS Comentario_Fecha,
		Com_Pre.Prestamo_Id AS Prestamo_Id
	FROM
		(SELECT 
			C.Id AS Comentario_Id, 
			C.Texto AS Comentario_Texto, 
			C.FechaCreacion AS Comentario_Fecha,
			C.Perfil_Id AS Perfil_Id,
			P.Id AS Prestamo_Id
		FROM
			Comentario AS C ,
			Prestamo AS P
		WHERE
			P.Id = inPrestamo_Id AND C.Prestamo_Id = P.Id) AS Com_Pre , Perfil
	WHERE Com_Pre.Perfil_Id = Perfil.Id
	ORDER BY Comentario_Fecha DESC;
END $$
