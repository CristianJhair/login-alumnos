USE MATRICULA_ADEX

CREATE TABLE tb_Aulas(
	nIdAula INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	nCapacidad INT NOT NULL

)


CREATE TABLE tb_Ciclo(
	nIdCiclo INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	nombre_ciclo NVARCHAR(50)
)


CREATE TABLE tb_Cursos(
	nIdCurso INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	sDesCurso VARCHAR(50),
	nIdCiclo INT FOREIGN KEY REFERENCES tb_Ciclo(nIdCiclo)
)


CREATE TABLE tb_Alumnos(
	nIdAlumno INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	nombreAlumno VARCHAR(50) NOT NULL,
	apellidoAlumno VARCHAR(50) NOT NULL,
	username NVARCHAR(50),
	password NVARCHAR(MAX),
	nIdCiclo INT FOREIGN KEY REFERENCES tb_Ciclo(nIdCiclo)
)

CREATE TABLE tb_Seccion(
	nIdSeccion INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	nIdAula INT FOREIGN KEY REFERENCES tb_Aulas(nIdAula),
	nIdCurso INT FOREIGN KEY REFERENCES tb_Cursos(nIdCurso),
	sTurno VARCHAR(50) NOT NULL,
	tHoraInicio TIME NOT NULL,
	tHoraFin TIME NOT NULL,
)

CREATE TABLE tb_Matricula(
	nIdAlumno INT FOREIGN KEY REFERENCES tb_Alumnos(nIdAlumno),
	nIdSeccion INT FOREIGN KEY REFERENCES tb_Seccion(nIdSeccion),
	PRIMARY KEY (nIdAlumno, nIdSeccion)
)


ALTER TABLE tb_Matricula
ADD nIdCurso INT;

ALTER TABLE tb_Matricula
ADD CONSTRAINT fk_nIdCurso
FOREIGN KEY (nIdCurso)
REFERENCES tb_Cursos(nIdCurso);


-----------------------INSERTS--------------------------------------
--CICLOS
INSERT INTO tb_Ciclo (nombre_ciclo)
VALUES ('Ciclo1'), ('Ciclo2'), ('Ciclo3'), ('Ciclo4'), ('Ciclo5'), ('Ciclo6')


--CURSOS
INSERT INTO tb_Cursos
VALUES ('Matematica 1',1),('Lenguaje 1',1), ('English 1',1),('Economia 1',1),
('Fisica',2),('Programacion basica',2),('Fundamentos de Finanzas',2),('English 2',2),
('Programacion Orientada a Objetos',3),('Finanzas avanzadas',3),('English 3',3),
('Inteligencia artificial',4),('English 4',4),('Exportaciones',4)

select * from tb_Cursos

--AULAS
INSERT INTO tb_Aulas
VALUES (2),(2),(2),(2)

--SECCIONES
INSERT INTO tb_Seccion
VALUES (1,1,'T','14:00:00','16:00:00'),(2,5,'M','08:00:00','10:00:00'),(3,9,'T','14:00:00','16:00:00'),(1,12,'N','20:00:00','22:00:00')

--select* from tb_Seccion

--MATRICULA
--INSERT INTO tb_Matricula
--VALUES (1,1)


-----------------------------------------------------------------------
--CONSULTA DE CURSOS DISPONIBLES

DECLARE @IdAlumnoSeleccionado INT = 2;

-- Obtener el ciclo actual del alumno seleccionado
DECLARE @CicloAct INT;
SELECT @CicloAct = nIdCiclo
FROM tb_Alumnos
WHERE nIdAlumno = @IdAlumnoSeleccionado;

-- Obtener los cursos disponibles para el ciclo actual del alumno seleccionado
SELECT @IdAlumnoSeleccionado AS nIdAlumno, nIdCiclo, C.nIdCurso, C.sDesCurso
FROM tb_Cursos C
WHERE C.nIdCiclo = @CicloAct
AND NOT EXISTS (
    SELECT 1
    FROM tb_Matricula M
    INNER JOIN tb_Seccion S ON M.nIdSeccion = S.nIdSeccion
    WHERE M.nIdAlumno = @IdAlumnoSeleccionado
    AND S.nIdCurso = C.nIdCurso
);

--PROCEDURE
GO
CREATE PROCEDURE ObtenerCursosDisponibles
	@IdAlumno INT
AS
BEGIN
    DECLARE @CicloActual INT

    -- Obtener el ciclo actual del alumno
    SELECT @CicloActual = nIdCiclo
    FROM tb_Alumnos
    WHERE nIdAlumno = @IdAlumno

    IF @CicloActual IS NOT NULL
    BEGIN
        -- Consulta para obtener los cursos disponibles en el ciclo actual
        SELECT C.nIdCurso, C.sDesCurso
        FROM tb_Cursos C
        WHERE C.nIdCiclo = @CicloActual
        AND C.nIdCurso NOT IN (
            SELECT M.nIdCurso
            FROM tb_Matricula M
            WHERE M.nIdAlumno = @IdAlumno
        )
    END
END

