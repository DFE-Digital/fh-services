import { Sequelize } from 'sequelize'
import '@dotenvx/dotenvx'

export const serviceDirectoryDb = buildSequelizeConnection(
  process.env.DB_SD_HOST, process.env.DB_SD_NAME, process.env.DB_SD_USER, process.env.DB_SD_PASSWORD)

export const referralDb = buildSequelizeConnection(
  process.env.DB_REFERRAL_HOST, process.env.DB_REFERRAL_NAME, process.env.DB_REFERRAL_USER, process.env.DB_REFERRAL_PASSWORD)

function buildSequelizeConnection (dbHost, dbName, dbUser, dbPassword) {
  console.log(`Building DB connection for ${dbName} @ ${dbHost} (user ${dbUser})`)
  return new Sequelize(dbName, dbUser, dbPassword, {
    host: dbHost,
    dialect: 'mssql'
  })
}

export async function closeConnections () {
  await serviceDirectoryDb.close()
}
