--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Estanteria_Perfiles_Mas_Cabrones $$

CREATE PROCEDURE sp_Estanteria_Perfiles_Mas_Cabrones (
	inLimit BIGINT
)
BEGIN
	SELECT
		per.Id,
        per.Nombre,
        COUNT(Pre.Receptor_Id) AS Cantidad
	FROM
		Perfil AS per
	INNER JOIN
		Prestamo as pre
	ON
		pre.Receptor_Id = per.Id
	WHERE
        pre.FechaEnvio BETWEEN DATE_SUB(NOW(), INTERVAL 30 DAY) AND NOW() AND pre.Estado = 2
	GROUP BY
		per.Id
    ORDER BY
		Cantidad DESC
	LIMIT inLimit;
END
$$

