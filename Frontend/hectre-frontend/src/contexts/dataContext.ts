import React from "react";
import { dataProvider } from "../providers";

const DataContext = React.createContext(dataProvider());
export default DataContext;
