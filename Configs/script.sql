CREATE TABLE public.medicos (
    id serial,
    crm varchar(10) NOT NULL,
    email varchar(500) NOT NULL,
    pass_hash varchar(1000) NULL,
    key_MFA varchar(1000) NULL,
    nome varchar(255) NOT NULL,
    CONSTRAINT PK_medicos PRIMARY KEY (id)
);

CREATE TABLE public.medicos_espec (
    id serial,
    id_medico int NOT NULL,
    especialidade varchar(100) NOT NULL,
    valor money NOT NULL,
    CONSTRAINT PK_medicos_espec PRIMARY KEY (id),
    CONSTRAINT fk_medicos_espec_medico FOREIGN KEY (id_medico) REFERENCES public.medicos(id)
);

CREATE TABLE public.medicos_agenda (
    id serial,
    id_medico int NOT NULL,
    data_agenda date NOT NULL,
    hora_agenda time NOT NULL,
    CONSTRAINT PK_medicos_agenda PRIMARY KEY (id),
    CONSTRAINT fk_medico_agenda_medicos FOREIGN KEY (id_medico) REFERENCES public.medicos(id)
);

CREATE TABLE public.pacientes (
    id serial,
    cpf varchar(11) NOT NULL,
    email varchar(500) NOT NULL,
    pass_hash varchar(1000) NULL,
    key_MFA varchar(1000) NULL,
    nome varchar(255) NOT NULL,
    CONSTRAINT PK_pacientes PRIMARY KEY (id)
);

CREATE TABLE public.pacientes_consulta (
    id serial,
    id_paciente int NOT NULL,
    id_medico_agenda int NOT NULL,
    id_medico_espec int NOT NULL,
    CONSTRAINT PK_pacientes_consulta PRIMARY KEY (id),
    CONSTRAINT fk_pacientes_consulta_pacientes FOREIGN KEY (id_paciente) REFERENCES public.pacientes(id),
    CONSTRAINT fk_pacientes_consulta_agenda FOREIGN KEY (id_medico_agenda) REFERENCES public.medicos_agenda(id),
    CONSTRAINT fk_pacientes_consulta_espec FOREIGN KEY (id_medico_espec) REFERENCES public.medicos_espec(id)
);

CREATE TABLE public.pacientes_consulta_rejeitada (
    id serial,
    id_paciente_consulta int NOT NULL,
    motivo text NOT NULL,
    CONSTRAINT PK_pacientes_consulta_rejeitada PRIMARY KEY (id),
    CONSTRAINT fk_pacientes_consulta_rejeitada_consulta FOREIGN KEY (id_paciente_consulta) REFERENCES public.pacientes_consulta(id)
);
