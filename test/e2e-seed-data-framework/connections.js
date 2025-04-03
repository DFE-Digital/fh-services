import { Sequelize } from "sequelize";
import { parseConnectionString } from "@tediousjs/connection-string";
import "@dotenvx/dotenvx";

export const serviceDirectoryDb = buildSequelizeConnection(
  process.env.CONNECTION_STRING_SERVICEDIRECTORY
);

export const referralDb = buildSequelizeConnection(
  process.env.CONNECTION_STRING_REFERRAL
);

export const reportDb = buildSequelizeConnection(
  process.env.CONNECTION_STRING_REPORT
);

function buildSequelizeConnection(connectionString) {
  const connectionStringParsed = parseConnectionString(connectionString);

  const database = connectionStringParsed["initial catalog"];
  const username = connectionStringParsed["user id"];
  const password = connectionStringParsed["password"];
  const host = connectionStringParsed["server"];
  const port = connectionStringParsed["port"];

  return new Sequelize(database, username, password, {
    dialect: "mssql",
    host: host,
    port: port,
  });
}

export async function checkConnections() {
  console.log("Checking Connections are OK...");

  try {
    await serviceDirectoryDb.authenticate();
    await referralDb.authenticate();
    await reportDb.authenticate();
  } catch (error) {
    console.error("Unable to establish connection to the database:", error);
  }

  console.log("Connections are OK!");
}

export async function closeConnections() {
  console.log("Closing Database Connections...");

  try {
    await serviceDirectoryDb.close();
    await referralDb.close();
    await reportDb.close();
  } catch (error) {
    console.error("Unable to close connection to the database:", error);
  }

  console.log("Connections Closed!");
}
