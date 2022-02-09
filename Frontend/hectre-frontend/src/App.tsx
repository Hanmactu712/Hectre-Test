import { Col, Layout, Row, Spin } from "antd";
import React, { Suspense } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import logo from "./hectre-logo.svg";
import { appRoutes } from "./routes";

const { Header, Content } = Layout;

function App() {
  return (
    // <div className="App">
    //   <header className="App-header">
    //     <img src={logo} className="App-logo" alt="logo" />
    //     <p>
    //       Edit <code>src/App.tsx</code> and save to reload.
    //     </p>
    //     <a
    //       className="App-link"
    //       href="https://reactjs.org"
    //       target="_blank"
    //       rel="noopener noreferrer"
    //     >
    //       Learn React
    //     </a>
    //   </header>
    // </div>
    <div className="App">
      <BrowserRouter>
        <Layout style={{ height: "100%" }}>
          <Header className="app-header">
            <Row justify="start">
              <Col flex="auto" style={{ width: "100%" }}>
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
  );
}

export default App;
