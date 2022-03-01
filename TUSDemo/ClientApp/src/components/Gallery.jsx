/*https://www.youtube.com/watch?v=aYZRRyukuIw&t=1s*/
import axios from "axios";
import { Card } from "proomkatest";
import { useEffect, useState } from "react";
import "./gall.css";

import { DragDropContext, Droppable, Draggable, resetServerContext } from "react-beautiful-dnd";

const Gallery = (props) => {
  const [images, setImages] = useState([]);
  const [loaded, setLoaded] = useState(false);
  
  const [characters, updateCharacters] = useState(images);

  useEffect(() => {
    axios
      .get("/api/Img")
      .then((resp) => {
        setImages(resp.data);
        updateCharacters(resp.data);
        setTimeout(() => {
          setLoaded(true);
        }, 3000);
      })
      .catch((err) => {
        // Handle Error Here
        console.error(err);
      });
  }, []);

  console.log(characters)
  function handleOnDragEnd(result){
    if(!result.destination)return;
    const items = Array.from(characters);
    const [reorderedItem] = items.splice(result.source.index, 1);
    items.splice(result.destination.index,0,reorderedItem);
    updateCharacters(items);
    const article = { title: 'menim poradi' };
    axios.put('/api/Img/' + result.source.index + "/" + result.destination.index , article)
        .then(response => console.log(response));

  }
  return (
    <>
      {loaded ? (
        <DragDropContext className="gallCont" onDragEnd={handleOnDragEnd}>
          <Droppable droppableId="characters">
            {(provided) => (
              <div className="gall" {...provided.droppableProps} ref={provided.innerRef}>
                {characters.map(( image, i) => {
                  return (
                    <Draggable key={i} draggableId={i.toString()} index={i}>
                      {(provided) => (
                        <div className="imgCard" {...provided.draggableProps} {...provided.dragHandleProps} ref={provided.innerRef}>
                        <Card /*key={i}*/ className="proomka-card card" >
                          <img
                            src={
                              "https://localhost:44487/api/Img/" +
                              image.storedFileId
                            }
                            alt="gallery-photo"
                          ></img>
                        </Card>
                        
                        </div>
                      )}
                    </Draggable>
                  );
                })}
                {provided.placeholder}
              </div>
            )}
          </Droppable>
        </DragDropContext>
      ) : (
        <>
          <Card className="loading proomka-card card"></Card>
          <Card className="loading proomka-card card"></Card>
          <Card className="loading proomka-card card"></Card>
          <Card className="loading proomka-card card"></Card>
        </>
      )}
    </>
  );
};

export default Gallery;
