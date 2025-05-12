CREATE TABLE IF NOT EXISTS public.client (
    id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active INT DEFAULT 1
);

CREATE TABLE IF NOT EXISTS public.shop (
    id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    type VARCHAR(100) NOT NULL,
    created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active INT DEFAULT 1
);

CREATE TABLE IF NOT EXISTS public.status (
    id UUID PRIMARY KEY,
    prefix VARCHAR(100) NOT NULL,
    description VARCHAR(100) NOT NULL,
    created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active INT DEFAULT 1
);

CREATE TABLE IF NOT EXISTS public.payment (
    id UUID PRIMARY KEY,
    concept TEXT NOT NULL,
    amount DECIMAL(10, 2),
    products_number INT NOT NULL,
    client_id UUID NOT NULL,
    shop_id UUID NOT NULL,
    status_id UUID NOT NULL,
    created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active INT DEFAULT 1,
    FOREIGN KEY (client_id) REFERENCES client(id) ON DELETE CASCADE,
    FOREIGN KEY (shop_id) REFERENCES shop(id) ON DELETE CASCADE,
    FOREIGN KEY (status_id) REFERENCES status(id) ON DELETE CASCADE
);

-- insert status
INSERT INTO public.status(id, prefix, description) 
VALUES ('a85d6b3a-0144-475d-8651-af7ef6cc5d68', 'APPROVED', 'APROBADO');
INSERT INTO public.status(id, prefix, description) 
VALUES ('cbce46d4-dd4e-4d4b-8b18-4c4c4fbab9b4', 'PENDING', 'PENDIENTE');
INSERT INTO public.status(id, prefix, description) 
VALUES ('986db470-188b-451f-9b5e-b26f621c8e58', 'CANCEL', 'CANCELADO');

-- insert client
INSERT INTO public.client(id, name, last_name)
VALUES ('4706bfc0-4dc8-4eb0-884f-8d39eb429fef', 'Jesus', 'Herrera');
INSERT INTO public.client(id, name, last_name)
VALUES ('1a9a0374-2f42-4872-846d-6513003726ca', 'Wendy', 'Herrera');

-- insert shop
INSERT INTO public.shop(id, name, type)
VALUES ('3db493dd-b7e8-4474-bac6-fa56614eb8c2', 'Samsung' ,'negocio');
INSERT INTO public.shop(id, name, type)
VALUES ('633ad6b0-81e2-4d36-8e18-369d49e9be7f', 'Papeleria la esquinita' ,'otro');