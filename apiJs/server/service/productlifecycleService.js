const productlifecycleData = require("../data/productlifecycleData")

exports.productlifeBy = function (id) {

    try {
        id = parseInt(id)
        if (isNaN(id)) throw new Error("Parametes as Not Number");
        return productlifecycleData.productlifeBy(id)
    } catch (e) {
        throw new Error(e.message);
    }
}

exports.productlifeUuid = function (uuid) {

    try {
        return productlifecycleData.productlifeUuid(uuid)
    } catch (e) {
        throw new Error(e.message);
    }
}


exports.productlifeAllYear = async function (year) {
    try {
        year = parseInt(year)
        if (isNaN(year)) throw new Error("Parametes as Not Number");
        return productlifecycleData.productlifeAllYear(year)
    } catch (e) {
        throw new Error(e.message);
    }
}




exports.productlifeYearMonthOrderQualitySold = async function (year, month, order) {
    try {
        year = parseInt(year)
        month = parseInt(month)
        if (isNaN(year) || isNaN(month)) throw new Error("Parametes as Not Number");
        return productlifecycleData.productlifeYearMonthOrderQualitySold(year, month, order)
    } catch (e) {
        throw new Error(e.message);
    }
}



exports.summaryMonthlySales = async function (year) {
    try {
        year = parseInt(year)
        if (isNaN(year)) throw new Error("Parametes as Not Number");
        return productlifecycleData.summaryMonthlySales(year)
    } catch (e) {
        throw new Error(e.message);
    }
}



exports.accumulatedProductsMonthlySales = async function (year) {
    try {
        year = parseInt(year)
        if (isNaN(year)) throw new Error("Parametes as Not Number");
        return productlifecycleData.accumulatedProductsMonthlySales(year)
    } catch (e) {
        throw new Error(e.message);
    }
}