import { Col, Layout, Row, Spin } from "antd";
import React, { Suspense } from "react";
import { QueryClient, QueryClientProvider } from "react-query";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import DataContext from "./contexts/dataContext";
import logo from "./hectre-logo.svg";
import { IDataProvider } from "./interfaces";
import { dataProvider } from "./providers";
import { appRoutes } from "./routes";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      queryKey: "*",
      staleTime: 1000 * 60,
    },
  },
});

const { Header, Content } = Layout;

function App({ dataContext }: { dataContext?: IDataProvider }) {
  return (
    <QueryClientProvider client={queryClient}>
      <DataContext.Provider value={dataContext || dataProvider()}>
        <div className="App">
          <BrowserRouter>
            <Layout style={{ height: "100%", background: "none" }}>
              <Header className="app-header">
                <Row justify="start" align="middle" style={{ height: "100%" }}>
                  <Col>
                    <a href="/">
                      <img src={logo} className="App-logo" alt="logo" />
                    </a>
                  </Col>
                </Row>
              </Header>
              <Content style={{ height: "100%" }}>
                <Suspense fallback={<Spin size="large"></Spin>}>
                  <Routes>
                    {appRoutes.map((route) => {
                      //console.log("route", route);
                      return <Route key={route.id} path={route.path} element={route.element}></Route>;
                    })}
                  </Routes>
                </Suspense>
              </Content>
            </Layout>
          </BrowserRouter>
        </div>
      </DataContext.Provider>
    </QueryClientProvider>
  );
}

export default App;
