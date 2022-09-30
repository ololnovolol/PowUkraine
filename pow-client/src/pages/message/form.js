import React, {useState, useRef} from 'react';
import styled from 'styled-components';
import axios from 'axios'

const SmButton = styled.button`
  background: #88A87D none repeat scroll 0% 0%;
  height: 40px;
  border-color: #fff;
  color: #fff;
  border-radius: 5px;
  align-items: center;
  font-size: 16px;
  &:hover {
    color: #F0A30A;
    background: #6D8764;
    cursor: pointer;
  }
`;

const BgButton = styled.button`
  background: #88A87D none repeat scroll 0% 0%;
  height: 40px;
  border-color: #fff;
  weight: 100%;
  position: relative;
  margin: right 10px;
  color: #F0A30A;
  border-radius: 5px;
  justify-content: space-between;
  font-size: 16px;
  &:hover {
    color: #fff;
    background: #6D8764;
    cursor: pointer;
  }
`;

const Block = styled.div`

  font-size: 1rem;
  position: relative;
  top: 3rem;
  border: 1 rem solid;

`;

const Form = styled.form`
  border-radius: 5px;
  }
`;

export const MessageForm = () => {
  const [data, loadData] = useState({
    Title: "",
    Description: "",
    Data: "",
    PhoneNumber: "",
    Attachment: "",

  });
  const filePicker = useRef(null);

  async function postMsg(e) {
    e.preventDefault();
    axios.post('https://localhost:44312/api/home/message',
    {
       Title: data.Title,
       Description: data.Description,
       Data: data.Data,
       PhoneNumber: data.PhoneNumber,
       Attachment: data.Attachment
     },
    {
      headers: {
        "Content-Type": "application/json"
      }
    })
    .then(function (response) {
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
    });
  }

  /*function handleData(e){
    const formData = new FormData();
    formData.append('Attachment',data.Attachment);
    loadData(formData);
  }
  */

  const handlePick = () => {
    filePicker.current.click();
  }

  function handle(e) {

    const newData={...data}
    newData[e.target.id] = e.target.value
    loadData(newData)
    console.log(newData)
  }



  

        return (
          <>
            <Form  onSubmit={postMsg}>
            <h1>Create important message</h1> 
                <Block>
                    <label>Title
                    <input id="Title" name="Title" onChange={(e) => handle(e)} value={data.Title} required/>   </label>
                    <label>Phone
                    <input id="PhoneNumber" name="PhoneNumber" onChange={(e) => handle(e)} value={data.PhoneNumber}
                        type="tel" placeholder="+380-99-77-77-777" /></label>
                    <label>Data
                    <input id="Data" name="Data" onChange={(e) => handle(e)} value={data.Data} type={"datetime-local"} required/></label>
                    <label>Description
                    <textarea id="Description" name="Description" onChange={(e) => handle(e)} value={data.Description} /></label> 
                <Block>
                    <label>Add Location</label>
                    <SmButton type='button'>Pin Location</SmButton>
                </Block>
                <Block>
                    <label htmlFor="images">Add File
                    <input id="Attachment" name="Attachment" className='hidden' 
                        onChange={(e) => handle(e)} value={data.Attachment}
                        type="file" accept="image/*" ref={filePicker}/>
                    </label>
                    <SmButton type="button" onClick={handlePick} >PinFile</SmButton>
                </Block>
                <Block>
                    <label>Submit</label>
                    <BgButton type="submit" onClick={postMsg}>Submit</BgButton>
                </Block>

                </Block>
            </Form>
          </>
        );
      };
  
  export default MessageForm;