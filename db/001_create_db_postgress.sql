CREATE TABLE public.events (
	id varchar(36) NOT NULL,
	orderid varchar(36) NOT NULL,
	code varchar(5) NOT NULL,
	fullcode varchar(50) NOT NULL,
	createdat timestamptz NOT NULL,
	acknowledged bool NOT NULL DEFAULT false,
	processed bool NOT NULL DEFAULT false,
	sended bool NOT NULL DEFAULT false,
	payload varchar NULL,
	CONSTRAINT events_pk PRIMARY KEY (id)
);