CREATE TABLE IF NOT EXISTS produtos (
  id    INT AUTO_INCREMENT PRIMARY KEY,
  nome  VARCHAR(100)  NOT NULL,
  preco DECIMAL(10,2) NOT NULL
);

INSERT INTO produtos (nome, preco)
VALUES ('Caderno', 19.90),
       ('Caneta', 3.50);
