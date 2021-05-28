CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR (255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR (255) COMMENT 'User Name',
  email VARCHAR (255) COMMENT 'User Email',
  picture VARCHAR (255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR (255) NOT NULL COMMENT 'FK: Account',
  name VARCHAR (255) NOT NULL COMMENT 'Vault Name',
  description VARCHAR (255) NOT NULL COMMENT 'Vault Description',
  isPrivate BOOLEAN NOT NULL COMMENT 'Vault Privacy',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR (255) NOT NULL COMMENT 'FK: Account',
  name VARCHAR (255) NOT NULL COMMENT 'Keep Name',
  description VARCHAR (255) NOT NULL COMMENT 'Keep Description',
  img VARCHAR (255) NOT NULL COMMENT 'Keep Image',
  views INT NOT NULL COMMENT 'Keep Views',
  shares INT NOT NULL COMMENT 'Keep Shares',
  keeps INT NOT NULL COMMENT 'Keep downloads',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vault_keeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR (255) NOT NULL COMMENT 'FK: Account',
  vaultId int NOT NULL COMMENT 'FK: Vault Relationship',
  keepId int NOT NULL COMMENT 'FK: Keep Relationship',
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
);