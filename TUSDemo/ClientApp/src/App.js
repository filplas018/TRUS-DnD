import styled from "styled-components";
import { BrowserRouter, Route, Switch, Link } from "react-router-dom";
import { Navigation } from "proomkatest";
import Upload from "./components/Upload";
import Gallery from "./components/Gallery";

const GlobalStyles = styled.div`
  min-height: 100vh;
  color: white;
  padding: 2rem 0;

  * {
    list-style: none;
    padding-left: 0;
  }

  li {
    display: flex;
    align-items: center;
    padding: 0.5em 0.8em 0.5em 0.5em;
    margin-bottom: 1em;
  }

  .opener {
    position: absolute;
    left: -3rem;
    top: 50%;
    width: 3rem;
    height: 3rem;
    background-color: #ffffff;
    display: grid;
    place-items: center;
    cursor: pointer;
  }

  .proomka-navigation {
    background-image: linear-gradient(to bottom right, #f5f7fab9, #c3cfe2b9);
    cursor: default;
    backdrop-filter: blur(10px);
    border-radius: 0;
  }
  .proomka-navigation-header {
    color: #5b9cda;
    text-align: center;

    &:hover {
      color: #5b9cda;
    }
  }
  .proomka-navigation-item {
    color: #6190bd;

    &:hover {
      color: #275683;
    }
  }

  .upload {
    display: grid;
    height: 100vh;
    width: 100%;

    align-items: center;
    justify-content: center;

    .split {
      display: flex;
      align-items: center;
      justify-content: space-between;
      height: 3.5rem;
      border: 0;
      font-size: 0.875rem;
      background: #17181b;
      color: #f7f7f7;
      cursor: pointer;
    }

    .split-button {
      display: grid;
      place-items: center;
      width: 3.5rem;
      height: inherit;
      padding: 0;
      border: 0;
      font-size: 0.75rem;
      background: #292c38;
      color: #f7f7f7;
      cursor: pointer;
    }

    .button {
      display: flex;
      align-items: center;
      gap: 0.75rem;
      width: 12rem;
      height: 3rem;
      padding-left: 1rem;
      border: 0;
      font-family: Poppins;
      text-align: left;
      color: #f7f7f7;
      background: transparent;
      outline: none;
      cursor: pointer;
    }

    .button-icon {
      font-size: 1.15rem;
      cursor: pointer;
    }

    #imagePost {
      display: none;
    }
  }

  .proomka-card {
    max-width: 80vw;
    margin: 0 auto;
    height: auto;
    border-radius: 0;
    width: 32rem;

    display: grid;
    align-items: center;

    img {
      width: 100%;
      height: 100%;

      object-fit: cover;
    }
  }

  p {
    font-size: 2rem;
    font-weight: 700;
    text-align: center;
  }

  .loading {
    cursor: progress;
    background: linear-gradient(0.25turn, transparent, #fff, transparent),
      linear-gradient(#eee, #eee);
    background-repeat: no-repeat;
    background-position: -315px 0, 0 0, 0px 190px, 50px 195px;
    animation: loading 1.5s infinite;
  }
  @keyframes loading {
    to {
      background-position: 315px 0, 0 0, 0 190px, 50px 195px;
    }
  }
`;

function App() {
  return (
    <BrowserRouter>
      <GlobalStyles>
        <Navigation className="proomka-navigation">
          <h2 className="proomka-navigation-header">TUS DEMO</h2>
          <Link to="/" className="proomka-navigation-item">
            Home
          </Link>
          <Link to="/upload" className="proomka-navigation-item">
            Upload
          </Link>
        </Navigation>
        <Switch>
          <Route exact path="/">
            <Gallery />
          </Route>
          <Route path="/upload">
            <Upload />
          </Route>
        </Switch>
      </GlobalStyles>
    </BrowserRouter>
  );
}

export default App;
