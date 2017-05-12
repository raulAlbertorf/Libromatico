CREATE TABLE IF NOT EXISTS Item(
	Id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
	Titulo VARCHAR(256),
    Disponibilidad BOOLEAN,
	Resumen TEXT,
	UrlImagen VARCHAR(256) DEFAULT "notPicture.jpg",
    VecesVisto BIGINT UNSIGNED,
    Perfil_Id BIGINT UNSIGNED NOT NULL,
    foreign key(Perfil_Id) references Perfil(Id) ON DELETE CASCADE
);